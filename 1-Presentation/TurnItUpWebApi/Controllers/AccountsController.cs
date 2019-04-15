﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Infrastructure.CrossCutting.Helpers;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Creates a new account within the system.
        /// </summary>
        /// <param name="registerDto">TBody containing password and email to create the account.</param>
        /// <returns></returns>
		[HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]RegisterCreateDto registerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var result = await this.userService.CreateUserAsync(registerDto, registerDto.Password);

			return new CreatedResult("1", "Account created");
		}

        /// <summary>
        /// Edits an existing account with additional info provided after the inital register.
        /// </summary>
        /// <param name="id">The id of the account to be edited.</param>
        /// <param name="editDto">TThe information to be used in the update. Used as body of the request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EditUserAsync([FromRoute] int id, [FromBody] RegisterEditDto editDto)
        {

            return null;
        }

		//[HttpPost]
		//[Route("login")]
		//public async Task<IActionResult> Login([FromBody]LoginDto loginDto) 
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	var identity = await this.userService.GetClaimsIdentity(loginDto.UserName, loginDto.Password);
		//	if (identity == null)
		//	{
		//		return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
		//	}

		//	var loginResponse = await this.userService
		//		.GenerateToken(
		//		identity,
		//		loginDto.UserName,
		//		loginDto.Password,
		//		loginDto.RemoteIpAddress,
		//		new JsonSerializerSettings { Formatting = Formatting.Indented }
		//		);

		//	return new OkObjectResult(loginResponse);
		//}

		//// POST api/auth/refreshtoken
		//[HttpPost("refreshtoken")]
		//public async Task<ActionResult> RefreshToken([FromBody] ExchangeRefreshTokenRequest request)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	return Ok(await this.userService.RefreshToken(request).ConfigureAwait(false));
		//}

		//[HttpPost]
		//[Route("{id}/claims")]
		//public async Task<IActionResult> AddClaim([FromRoute] int customerId, [FromQuery] string claimType,
		//	[FromQuery] string claimValue)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	var identity = HttpContext.User.Identity as ClaimsIdentity;

		//	await this.userService.AddClaimToUser(identity, claimType, claimValue);

		//	return Ok();
		//}
	}
}
