using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/musicians")]
    [ApiController]
    public class MusiciansController
    {
        public MusiciansController()
        {

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMusician()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateMusician()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}/digitalContents")]
        public async Task<IActionResult> UploadContent()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}/offers")]
        public async Task<IActionResult> CreateOfferAsync()
        {
            throw new NotImplementedException();
        }
    }
}
