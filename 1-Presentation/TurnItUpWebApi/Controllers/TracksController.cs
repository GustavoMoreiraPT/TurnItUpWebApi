using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/{id}/tracks")]
    public class TracksController : Controller
    {
        public ITrackService trackService;

        public TracksController(ITrackService trackService)
        {
            this.trackService = trackService;
        }

        /// <summary>
        ///  Uploads an audio file related to the given account.
        /// </summary>
        /// <param name="id"> The id of the account to add a track.</param>
        /// <param name="audioTrack">The audio file to be uploaded</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadFile([FromRoute] int id, [FromBody]Track audioTrack)
        {
            //check for identityId here
            await this.trackService.UploadTrack(id, audioTrack);

            return this.Ok();
        }
    }
}
