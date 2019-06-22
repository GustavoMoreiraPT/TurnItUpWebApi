using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/{id}/tracks/{trackId}/likes")]
    public class TrackLikesController : Controller
    {
        private readonly ITrackLikesService trackLikesService;

        public TrackLikesController(ITrackLikesService trackLikesService)
        {
            this.trackLikesService = trackLikesService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        public async Task<IActionResult> CreateTrackLike(
            [FromRoute] Guid id,
            [FromRoute] int trackId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            if (trackId < 1)
            {
                return this.BadRequest("Track id needs to be bigger than 0");
            }

            var result = await this.trackLikesService.LikeTrack(id, trackId).ConfigureAwait(false);

            if (result.TrackNotFound)
            {
                return this.NotFound();
            }

            return this.Accepted();
        }

        [HttpDelete]
        [Route("{likeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 5)]
        public async Task<IActionResult> DeleteTrackLike(
            [FromRoute] Guid id,
            [FromRoute] int trackId,
            [FromRoute] int likeId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userIdFromToken = identity.Claims.FirstOrDefault(x => x.Type == "id");

            var claimValue = userIdFromToken.Value;

            if (claimValue != id.ToString())
            {
                return this.StatusCode(403);
            }

            if (trackId < 1 || likeId < 1)
            {
                return this.BadRequest("Track id needs to be bigger than 0");
            }

            var result = await this.trackLikesService.DeleteTrackLike(id, trackId, likeId).ConfigureAwait(false);

            if (!result.IsTrackFound || !result.IsDeleted)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
