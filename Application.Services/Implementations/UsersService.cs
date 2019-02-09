using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repository.Configuration;
using Domain.Model.Users;
using Infrastructure.CrossCutting.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
