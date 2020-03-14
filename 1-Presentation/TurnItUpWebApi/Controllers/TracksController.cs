using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;
using Microsoft.AspNetCore.Http.Features;
using Application.Dto;
using System.Collections.Generic;
using Application.Dto.Tracks.Pages;

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
        /// <param name="trackPhoto"> The photo to be saved alongside the track.</param>
        /// <param name="track">The audio file to be uploaded.</param>
        /// <param name="createTrackRequest">Some adittional info for the track</param>
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
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromRoute] Guid id, [FromForm]CreateTrackRequest createTrackRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            if (createTrackRequest.Track == null || createTrackRequest.Track.Length == 0)
                return Content("file not selected");

            var result = await this.trackService.UploadTrack(id, createTrackRequest.Track, createTrackRequest);

            if (result.Errors.Any())
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        /// <summary>
        ///  Uploads an audio file related to the given account.
        /// </summary>
        /// <param name="id"> The id of the account to add a track.</param>
        /// <param name="trackId"> The if of the track to upload the photo</param>
        /// <param name="photo">The photo from the body to be added.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{trackId}/photo")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        [Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadTrackPhoto([FromRoute] Guid id, int trackId, [FromBody]Photo photo)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            var result = await this.trackService.UploadTrackPhoto(id, trackId, photo).ConfigureAwait(false);

            if (result.Errors.Any())
            {
                return this.BadRequest(result);
            }

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
        public async Task<IActionResult> DeleteTrack([FromRoute] Guid id, [FromRoute] int trackId)
        {
            if (id == Guid.Empty ||trackId < 1)
            {
                return this.BadRequest();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            var result = await this.trackService.DeleteTrack(id, trackId);

            if (result == false)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }

        /// <summary>
        ///  Getsall tracks informations for a specific user.
        /// </summary>
        /// <param name="id"> The id of the account to read tracks information.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TrackInfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        public async Task<IActionResult> GetTracks([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.BadRequest();
            }

            var tracksInfo = await this.trackService.GetTracksInfo(id).ConfigureAwait(false);

            if (tracksInfo == null)
            {
                return this.NotFound();
            }

            return this.Ok(tracksInfo);
        }

        /// <summary>
        ///  Getsall tracks informations for a specific user.
        /// </summary>
        /// <param name="id"> The id of the account to read tracks information.</param>
        /// <param name="pageNumber"> The page number wanted.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(TrackInfoPage), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        [Route("Page")]
        public async Task<IActionResult> GetTracksWithPagination([FromRoute] Guid id, [FromQuery]int pageNumber)
        {
            throw new NotImplementedException();
        }
    }
}
