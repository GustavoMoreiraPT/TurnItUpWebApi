using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Handlers
{
	public interface IJwtTokenHandler
	{
		string WriteToken(JwtSecurityToken jwt);
		ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters);
	}
}
