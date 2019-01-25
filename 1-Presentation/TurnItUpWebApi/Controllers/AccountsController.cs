using Application.Requests.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Users;
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
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;
	
		public AccountsController(
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			IConfiguration configuration
			)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.configuration = configuration;
		}

		[HttpPost]
		[Route("login")]
		public async Task<object> LoginAsync([FromBody] LoginDto model)
		{
			var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

			if (result.Succeeded)
			{
				var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);

				var userRoles = await userManager.GetRolesAsync(appUser).ConfigureAwait(false);

				var userClaims = await this.userManager.GetClaimsAsync(appUser).ConfigureAwait(false);

				return await GenerateJwtToken(model.Email, appUser, userRoles.ToList(), userClaims.ToList());
			}

			throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "INVALID_LOGIN_ATTEMPT");
		}

		[HttpPost]
		[Route("register")]
		public async Task<object> RegisterAsync([FromBody] RegisterDto model)
		{
			var user = new IdentityUser
			{
				UserName = model.Email,
				Email = model.Email
			};

			var result = await userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				await signInManager.SignInAsync(user, false);

				return await GenerateJwtToken(model.Email, user);
			}

			throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "UNKNOWN_ERROR");
		}

		[HttpPost]
		[Route("{id}/claims")]
		public async Task<object> AddClaimAsync([FromRoute] Guid id, [FromBody]NewClaimRequest claim)
		{
			var user = await this.userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);

			if (id != Guid.Parse(user.Id))
			{
				return this.BadRequest("The id sent in the request is not the same of the authenticated user");
			}

			var x = Enum.GetNames(typeof(AvailableClaims));

			if (Enum.GetNames(typeof(AvailableClaims)).Contains(claim.Name))
			{
				await this.userManager.AddClaimAsync(user, new Claim(claim.Name, claim.Value));

				var userRoles = await userManager.GetRolesAsync(user).ConfigureAwait(false);

				var userClaims = await this.userManager.GetClaimsAsync(user).ConfigureAwait(false);

				return await GenerateJwtToken(user.Email, user, userRoles.ToList(), userClaims.ToList());
			}

			return this.BadRequest();
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
