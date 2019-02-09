using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TurnItUpWebApi.Controllers
{
	[Route("v1/digitalContents")]
	[Authorize]
	public class DigitalContentsController : Controller
	{
		public DigitalContentsController()
		{

		}

		[HttpGet]
		public async Task<IActionResult> GetDigitalContentsAsync()
		{
			throw new NotImplementedException();
		}

		[HttpPatch]
		[Route("{id}")]
		public async Task<IActionResult> PartialUpdateAsync()
		{
			throw new NotImplementedException();
		}
	}
}
