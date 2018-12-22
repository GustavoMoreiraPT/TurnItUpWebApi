using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/digitalContents")]
    [ApiController]
    public class DigitalContentsController
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
