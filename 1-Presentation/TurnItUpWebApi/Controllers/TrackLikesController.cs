using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/{id}/tracks{trackId}/likes")]
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
        public async Task CreateTrackLike(
            [FromRoute] Guid id,
            [FromRoute] int trackId,
            [FromBody] TrackLikeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
