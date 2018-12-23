using Application.Dto.Musicians;
using Application.Requests.Musicians;
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
        [ProducesResponseType(200, Type = typeof(Musician))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetMusician([FromRoute] int id)
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
        public async Task<IActionResult> UpdateMusician([FromRoute] int id, [FromBody] UpdateMusicianRequest updateMusicianRequest)
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
