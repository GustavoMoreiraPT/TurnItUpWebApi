using Application.Requests.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using Domain.Model.Users;
using Infrastructure.CrossCutting.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Remotion.Linq.Clauses.ResultOperators;

namespace TurnItUpWebApi.Controllers
{
	[Route("v1/accounts")]
	public class AccountsController : Controller
	{
		private readonly IUsersService userService;
		private readonly IConfiguration configuration;
	
		public AccountsController(
			IUsersService userService,
			IConfiguration configuration
			)
		{
			this.userService = userService;
			this.configuration = configuration;
		}


		[HttpPost]
		public async Task<IActionResult> Post([FromBody]RegisterDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await this.userService.CreateUserAsync(model, model.Password);

			return new OkObjectResult("Account created");
		}

		//[HttpPost]
		//public IActionResult Refresh(string token, string refreshToken)
		//{
		//	var principal = GetPrincipalFromExpiredToken(token);
		//	var username = principal.Identity.Name;
		//	var savedRefreshToken = GetRefreshToken(username); //retrieve the refresh token from a data store
		//	if (savedRefreshToken != refreshToken)
		//		throw new SecurityTokenException("Invalid refresh token");

		//	var newJwtToken = GenerateJwtToken(null, null, null, principal.Claims.ToList());
		//	var newRefreshToken = GenerateRefreshToken();
		//	DeleteRefreshToken(username, refreshToken);
		//	SaveRefreshToken(username, newRefreshToken);

		//	return new ObjectResult(new
		//	{
		//		token = newJwtToken,
		//		refreshToken = newRefreshToken
		//	});
		//}

		private void SaveRefreshToken(string username, string newRefreshToken)
		{
			throw new NotImplementedException();
		}

		private void DeleteRefreshToken(string username, string refreshToken)
		{
			throw new NotImplementedException();
		}

		private object GetRefreshToken(string username)
		{
			throw new NotImplementedException();
		}

		[HttpGet]
		[Route("protected")]
		[Authorize]
		public async Task<object> Protected()
		{
			return "Protected area";
		}

		private async Task<object> GenerateJwtToken(string email, IdentityUser user, List<string> roles = null, List<Claim> userClaims = null)
		{
			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
			};

			if (userClaims != null)
			{
				claims.AddRange(userClaims);
			}

			if (roles != null)
			{
				claims.AddRange(this.AddRolesClaims(roles));
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:JwtKey"]));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:JwtExpireDays"]));

			var token = new JwtSecurityToken(
				configuration["JwtSettings:JwtIssuer"],
				configuration["JwtSettings:JwtIssuer"],
				claims,
				expires: expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the server key used to sign the JWT token is here, use more than 16 chars")),
				ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
			var jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		private List<Claim> AddRolesClaims(List<string> roleNamesList)
		{
			var roleClaims = new List<Claim>();

			foreach (var role in roleNamesList)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			return roleClaims;
		}
	}
}
