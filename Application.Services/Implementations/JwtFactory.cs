using Infrastructure.CrossCutting.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Principal;
using Application.Services.Interfaces;
using Application.Dto.Users;
using Application.Services.Handlers;

namespace Application.Services.Implementations
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IJwtTokenHandler jwtHandler;

        public JwtFactory(
	        IOptions<JwtIssuerOptions> jwtOptions,
			IJwtTokenHandler jwtHandler
	        )
        {
            _jwtOptions = jwtOptions.Value;
            this.jwtHandler = jwtHandler;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<AccessToken> GenerateEncodedToken(string id, string userName)
        {
	        var identity = GenerateClaimsIdentity(id, userName);

	        var claims = new[]
	        {
		        new Claim(JwtRegisteredClaimNames.Sub, userName),
		        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
		        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
		        identity.FindFirst(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
		        identity.FindFirst(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
	        };

	        // Create the JWT security token and encode it.
	        var jwt = new JwtSecurityToken(
		        _jwtOptions.Issuer,
		        _jwtOptions.Audience,
		        claims,
		        _jwtOptions.NotBefore,
		        _jwtOptions.Expiration,
		        _jwtOptions.SigningCredentials);

	        return new AccessToken(this.jwtHandler.WriteToken(jwt), (int)_jwtOptions.ValidFor.TotalSeconds);
        }

		public async Task<AccessToken> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var claims = new[]
         {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessToken(encodedJwt, (int)_jwtOptions.ValidFor.TotalSeconds);
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Infrastructure.CrossCutting.Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
