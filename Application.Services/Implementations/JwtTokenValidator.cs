using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Application.Services.Handlers;
using Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Implementations
{
	internal sealed class JwtTokenValidator : IJwtTokenValidator
	{
		private readonly IJwtTokenHandler jwtTokenHandler;

		internal JwtTokenValidator(IJwtTokenHandler jwtTokenHandler)
		{
			this.jwtTokenHandler = jwtTokenHandler;
		}

		public ClaimsPrincipal GetPrincipalFromToken(string token, string signingKey)
		{
			var claimsPrincipal = this.jwtTokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
				ValidateLifetime = false
			});

			return claimsPrincipal;
		}
	}
}
