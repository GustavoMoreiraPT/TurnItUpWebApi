using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/digitalContents")]
    [ApiController]
	[Authorize]
    public class DigitalContentsController
    {
        public DigitalContentsController()
        {

        }

        [HttpGet]
        [Authorize(Roles = "Politicians")]
		public async Task<IActionResult> GetDigitalContentsAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{id}")]
        [Authorize/*(Policy = "musician")*/]
		public async Task<IActionResult> PartialUpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
