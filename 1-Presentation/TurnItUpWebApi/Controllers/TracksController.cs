﻿using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;

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
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
        public async Task<IActionResult> UploadFile([FromRoute] int id, [FromBody]Track audioTrack)
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
    }
}
