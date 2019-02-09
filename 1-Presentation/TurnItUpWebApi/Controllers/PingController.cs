using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TurnItUpWebApi.Controllers
{
	[Route("v1/ping")]
	public class PingController : Controller
	{
		public PingController()
		{

		}

		[HttpGet]
		public IActionResult GetPing()
		{
			return new OkObjectResult("Ping");
		}

		[HttpGet]
		[Route("pong")]
		[Authorize(Policy = "PongViewer")]
		public IActionResult GetPong()
		{
			var x = this.HttpContext;
			return new OkObjectResult("Pong");
		}
	}
}
