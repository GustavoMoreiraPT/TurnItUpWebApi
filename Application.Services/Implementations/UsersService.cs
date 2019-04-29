using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Application.Dto.Enum;
using Application.Dto.Musicians;
using Application.Dto.Users;
using Application.Dto.Users.Responses;
using Application.Services.Interfaces;
using Application.Services.Specifications;
using AutoMapper;
using Data.Repository.Configuration;
using Domain.Core.RepositoryInterfaces;
using Domain.Model;
using Domain.Model.Users;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Helpers;
using Infrastructure.CrossCutting.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static Infrastructure.CrossCutting.Helpers.FacebookApiResponses;

namespace Application.Services.Implementations
{
	public class UsersService : IUsersService
	{
		private readonly ApplicationDbContext identityDbContext;
		private readonly UserManager<AppUser> userManager;
		private readonly IJwtFactory jwtFactory;
		private readonly IMapper mapper;
		private readonly JwtIssuerOptions jwtOptions;
		private readonly ITokenFactory tokenFactory;
		private readonly IJwtTokenValidator jwtTokenValidator;
		private readonly IRepository<Customer> repository;

		public UsersService(
			ApplicationDbContext identityDbContext,
			UserManager<AppUser> userManager,
			IMapper mapper,
			IJwtFactory jwtFactory,
			IOptions<JwtIssuerOptions> jwtOptions,
			ITokenFactory tokenFactory,
			IJwtTokenValidator jwtTokenValidator,
			IRepository<Customer> repository
			)
		{
			this.identityDbContext = identityDbContext;
			this.userManager = userManager;
			this.mapper = mapper;
			this.jwtFactory = jwtFactory;
			this.jwtOptions = jwtOptions.Value;
			this.tokenFactory = tokenFactory;
			this.jwtTokenValidator = jwtTokenValidator;
			this.repository = repository;
		}

		public async Task<string> AddRefreshToken(string token, string userName, string remoteIpAddress, double daysToExpire = 5)
		{
			var user = await this.userManager.FindByEmailAsync(userName).ConfigureAwait(false);

			if (user == null)
			{
				return string.Empty;
			}

			var customerUser = this.identityDbContext
				.Customers
				.FirstOrDefault(x => x.IdentityId == user.Id);

			if (customerUser == null)
			{
				return string.Empty;
			}

			var refreshToken = this.tokenFactory.GenerateToken();

			customerUser.AddRefreshToken(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(daysToExpire), userName, remoteIpAddress));

			this.identityDbContext.Customers.Update(customerUser);

			await this.identityDbContext.SaveChangesAsync();

			return refreshToken;
		}

		public async Task<RegisterResponseDto> CreateUserAsync(RegisterCreateDto user, string password)
		{
			var userIdentity = this.mapper.Map<AppUser>(user);

			var result = await this.userManager.CreateAsync(userIdentity, password).ConfigureAwait(false);

            if (result.Errors.Any())
            {
               return this.FillRegisterErrors(result.Errors.ToList());
            }

			await this.userManager.AddClaimAsync(userIdentity,
				new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));

			await this.userManager.AddClaimAsync(userIdentity,
				new Claim(Constants.Strings.JwtClaimIdentifiers.Events, Constants.Strings.JwtClaims.EventsAccess));

			if (!result.Succeeded)
			{
				return null;
			}

			var customer = await this.identityDbContext.Customers.AddAsync(new Customer{ IdentityId = userIdentity.Id, /*Location = user.Location*/});

			await this.CreateAccountType(customer, user);
			await this.identityDbContext.SaveChangesAsync();

			return new RegisterResponseDto
            {
                IdentityResult = result,
                UserCreatedId = customer.Entity.Id.ToString(),
                Errors = new List<Error>(),
            };
		}

        private RegisterResponseDto FillRegisterErrors(List<IdentityError> errors)
        {
            var apiErrors = new List<Error>();

            foreach (var error in errors)
            {
                var newError = new Error(error.Code, error.Description);
                apiErrors.Add(newError);
            }

            return new RegisterResponseDto
            {
                Errors = apiErrors
            };
        }

        private async Task CreateAccountType(EntityEntry<Customer> customer, RegisterCreateDto user)
		{
			//if (user.AccountType == AccountTypes.Musician)
			//{
			//	await this.CreateTurnItUpUser(customer.Entity, "Musician").ConfigureAwait(false);
			//}

			//if (user.AccountType == AccountTypes.Recruiter)
			//{
			//	await this.CreateTurnItUpUser(customer.Entity, "Recruiter").ConfigureAwait(false);
			//}
		}

		public async Task<IdentityResult> CreateUserAsync(AppUser user, FacebookUserData facebookUserData, string password)
		{
			var result =  await this.userManager.CreateAsync(user, password).ConfigureAwait(false);

			if (!result.Succeeded) return null;

			await this.identityDbContext.Customers
				.AddAsync(new Customer
				{
					IdentityId = user.Id, Location = "",
					Locale = facebookUserData.Locale,
					Gender = facebookUserData.Gender
				});

			await this.identityDbContext.SaveChangesAsync();

			return result;
		}

		public async Task<AppUser> FindByEmailAsync(string email)
		{
			return await this.userManager.FindByEmailAsync(email).ConfigureAwait(false);
		}

		public async Task<AppUser> FindByNameAsync(string email)
		{
			return await this.FindByNameAsync(email).ConfigureAwait(false);
		}

		public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
		{
			return this.jwtFactory.GenerateClaimsIdentity(userName, id);
		}

		public async Task<LoginResponse> GenerateToken(
			ClaimsIdentity identity,
			string userName,
			string password,
			string remoteIpAddress,
			JsonSerializerSettings serializerSettings
			)
		{
			var response = new
			{
				id = identity.Claims.Single(c => c.Type == "id").Value,
				auth_token = await this.jwtFactory.GenerateEncodedToken(userName, identity),
				expires_in = (int)jwtOptions.ValidFor.TotalSeconds
			};

			var accessToken = await 
				this.jwtFactory
				.GenerateEncodedToken(userName, identity).ConfigureAwait(false);

			var refreshToken = await
				this.AddRefreshToken(accessToken.Token, userName, remoteIpAddress)
				.ConfigureAwait(false);

			return new LoginResponse(accessToken, refreshToken, true);
		}

        public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await this.userManager.FindByNameAsync(userName);

            var userClaims = await this.userManager.GetClaimsAsync(userToVerify);

            userClaims.Add(new Claim(Constants.Strings.JwtClaimIdentifiers.Id, userToVerify.Id));


            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await this.userManager.CheckPasswordAsync(userToVerify, password))
            {
                return new ClaimsIdentity(new GenericIdentity(userName, "Token"), userClaims);
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public Task<int> GetCustomerIdByToken(string token)
		{
			throw new NotImplementedException();
		}

		public async Task AddClaimToUser(ClaimsIdentity identity, string claimType, string claimValue)
		{
			var userEmail = identity.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;

			var identityUser = await FindByEmailAsync(userEmail).ConfigureAwait(false);

			if (identityUser == null)
			{
				throw new ArgumentNullException();
			}

			await this.userManager.AddClaimAsync(identityUser, new Claim(claimType, claimValue));
		}

		//FINISH THIS
		public async Task<LoginResponse> RefreshToken(ExchangeRefreshTokenRequest refreshTokenRequest)
		{
			var claimPrincipal =
				this.jwtTokenValidator.GetPrincipalFromToken(refreshTokenRequest.AccessToken,
					"ArminVanBuurenMottyzeRuleTheWorldForever2010");

			if (claimPrincipal != null)
			{
				var id = claimPrincipal.Claims.First(c => c.Type == "id");
				var user = await this.repository.GetSingleBySpec(new UserSpecification(id.Value));

				var appUser = await this.userManager.FindByIdAsync(user.IdentityId).ConfigureAwait(false);

				if (user.HasValidRefreshToken(refreshTokenRequest.RefreshToken))
				{
					var jwtToken = await this.jwtFactory.GenerateEncodedToken(user.IdentityId, appUser.Email);
					var refreshToken = this.tokenFactory.GenerateToken();
					user.RemoveRefreshToken(refreshTokenRequest.RefreshToken); // delete the token we've exchanged
					user.AddRefreshToken(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(5), user.Identity.Email, string.Empty)); // add the new one
					await this.repository.Update(user);
					return new LoginResponse(jwtToken, refreshToken, true);
				}
			}

			return null;
		}

        public Task<RegisterEditResponseDto> EditUserAsync(RegisterEditDto user)
        {
            throw new NotImplementedException();
        }
    }
}
