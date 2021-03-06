﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.CrossCutting.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Handlers
{
	public sealed class JwtTokenHandler : IJwtTokenHandler
	{
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
		private readonly ILogger logger;

		public JwtTokenHandler(ILogger logger)
		{
			if (this.jwtSecurityTokenHandler == null)
			{
				this.jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			}

			this.logger = logger;
		}
		public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
		{
			try
			{
				var principal = this.jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

				if (!(securityToken is JwtSecurityToken jwtSecurityToken)
				    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) 
				{
					throw new SecurityTokenException("Invalid token");
				}

				return principal;
			}
			catch (Exception e)
			{
				this.logger.LogError($"Token validation failed: {e.Message}");
				return null;
			}
		}

		public string WriteToken(JwtSecurityToken jwt)
		{
			return this.jwtSecurityTokenHandler.WriteToken(jwt);
		}
	}
}
