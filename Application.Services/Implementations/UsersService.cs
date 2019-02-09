using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repository.Configuration;
using Domain.Model;
using Domain.Model.Users;
using Infrastructure.CrossCutting.Helpers;
using Infrastructure.CrossCutting.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsersService(
            ApplicationDbContext identityDbContext,
            UserManager<AppUser> userManager,
            IMapper mapper,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
		{
			this.identityDbContext = identityDbContext;
			this.userManager = userManager;
			this.mapper = mapper;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
		}

        public async Task AddRefreshToken(string token, string userName, string remoteIpAddress, double daysToExpire = 5)
        {
            var user = await this.userManager.FindByEmailAsync(userName).ConfigureAwait(false);

            if (user == null)
            {
                return;
            }

            var customerUser = this.identityDbContext
                .Customers
                .FirstOrDefault(x => x.IdentityId == user.Id);

            if (customerUser == null)
            {
                return;
            }

            customerUser.AddRefreshToken(new RefreshToken(token, DateTime.UtcNow.AddDays(daysToExpire), userName, remoteIpAddress));

            await this.identityDbContext.SaveChangesAsync();
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterDto user, string password)
		{
			var userIdentity = this.mapper.Map<AppUser>(user);

			var result = await this.userManager.CreateAsync(userIdentity, password).ConfigureAwait(false);

			if (!result.Succeeded)
			{
				return null;
			}

			await this.identityDbContext.Customers.AddAsync(new Customer{ IdentityId = userIdentity.Id, Location = user.Location});
			await this.identityDbContext.SaveChangesAsync();

			return result;
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

        public async Task<string> GenerateToken(ClaimsIdentity identity, string userName, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await this.jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        public async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await this.userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await this.userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(this.jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
