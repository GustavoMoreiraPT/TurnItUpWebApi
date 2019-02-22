using Application.Dto.Musicians;
using Application.Requests.Musicians;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TurnItUpWebApi.Controllers
{
	[Route("v1/musicians")]
	public class MusiciansController : Controller
	{
		private readonly IMusicianService musicianService;

		public MusiciansController(IMusicianService musicianService)
		{
			this.musicianService = musicianService;
		}

		[HttpGet]
		[Route("{id}/about")]
		[ProducesResponseType(200, Type = typeof(MusicianAboutDto))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[ProducesResponseType(403)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> CreateOrUpdateMusicianDetailsAsync([FromRoute] int id, [FromBody]MusicianAboutDto details)
		{
			if (id < 1)
			{
				return this.BadRequest("Musician ID must be greater than 0");
			}

			if (id != details.Id)
			{
				return this.BadRequest("Musician ID from body and from Route do not correspond. Please fix it and try again");
			}

			var accesToken = Request.Headers["Authorization"];

			return this.Ok(await this.musicianService.CreateOrUpdateMusicianDetails(details).ConfigureAwait(false));
		}

		[HttpGet]
		[Route("{id}/about")]
		[ProducesResponseType(200, Type = typeof(MusicianAboutDto))]
		[ProducesResponseType(400)]
		[ProducesResponseType(401)]
		[ProducesResponseType(403)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetMusicianDetailsAsync([FromRoute] int id)
		{
			if (id < 1)
			{
				return this.BadRequest("Musician ID must be greater than 0");
			}

			return this.Ok(await this.musicianService.GetMusicianDetails(id).ConfigureAwait(false));
		}

		[HttpPut]
		[Route("{id}")]
		[ProducesResponseType(200)]
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
		public async Task<IActionResult> UploadContentAsync()
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
