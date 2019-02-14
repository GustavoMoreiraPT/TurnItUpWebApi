﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Infrastructure.CrossCutting.Helpers;
using Newtonsoft.Json;

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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await this.userService.GetClaimsIdentity(loginDto.UserName, loginDto.Password);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
            }

            var loginResponse = await this.userService
                .GenerateToken(
                identity,
                loginDto.UserName,
                loginDto.Password,
                loginDto.RemoteIpAddress,
                new JsonSerializerSettings { Formatting = Formatting.Indented }
                );

            return new OkObjectResult(loginResponse);
        }

		[HttpGet]
		[Route("protected")]
        [Authorize(Policy = "ApiUser")]
        public async Task<object> Protected()
		{
			return "Protected area";
		}
	}
}
