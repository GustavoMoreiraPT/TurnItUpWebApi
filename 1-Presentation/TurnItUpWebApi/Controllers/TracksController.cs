using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Globalization;
using TurnItUpWebApi.Filters;
using Infrastructure.CrossCutting.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/{id}/tracks")]
    public class TracksController : Controller
    {
        public ITrackService trackService;

        private static readonly FormOptions defaultFormOptions = new FormOptions();

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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
        public async Task<IActionResult> UploadFile([FromRoute] Guid id, [FromBody]Track audioTrack)
        {
            //check for identityId here

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            await this.trackService.UploadTrack(id, audioTrack);

            return this.Ok();
        }

        /// <summary>
        ///  Uploads an audio file related to the given account.
        /// </summary>
        /// <param name="id"> The id of the account to add a track.</param>
        /// <param name="audioTrack">The audio file to be uploaded</param>
        /// <returns></returns>
        [HttpPost]
        [Route("test")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
          [DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        public async Task<IActionResult> UploadFileTest([FromRoute] Guid id, [FromBody]IFormFile track)
        {
            if (track == null || track.Length == 0)
                return Content("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        track.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await track.CopyToAsync(stream);
            }

            var result = await this.trackService.UploadTrack(id, null);

            return this.Ok(result);
        }

        /// <summary>
        ///  Deletes an audio file for a certain user.
        /// </summary>
        /// <param name="id"> The id of the account to add a track.</param>
        /// <param name="trackId">The id of the track to delete</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{trackId}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
        public async Task<IActionResult> DeleteTrack([FromRoute] int id, [FromRoute] int trackId)
        {
            throw new NotImplementedException();
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            MediaTypeHeaderValue mediaType;
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }
    }
}
