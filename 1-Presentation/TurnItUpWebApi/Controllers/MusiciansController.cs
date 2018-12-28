using Application.Dto.Musicians;
using Application.Requests.Musicians;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesResponseType(200, Type = typeof(Musician))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Musician")]
		public async Task<IActionResult> GetMusicianAsync([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(Musician))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Authorize/*(Policy = "musician")*/]
		public async Task<IActionResult> UpdateMusicianAsync([FromRoute] int id, [FromBody] UpdateMusicianRequest updateMusicianRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}/digitalContents")]
        [Authorize/*(Policy = "musician")*/]
		public async Task<IActionResult> UploadContentAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}/offers")]
        [Authorize/*(Policy = "musician")*/]
		public async Task<IActionResult> CreateOfferAsync()
        {
            throw new NotImplementedException();
        }
    }
}
