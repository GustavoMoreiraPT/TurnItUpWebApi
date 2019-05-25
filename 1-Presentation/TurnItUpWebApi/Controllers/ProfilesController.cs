
using Application.Dto.Profile;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;
using TurnItUpWebApi.ResponseModels;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/{id}/profiles")]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        /// <summary>
        /// Get the summary profile of a certain user.
        /// </summary>
        /// <param name="id">The id of the user to read the profile.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("summary")]
        [ProducesResponseType(typeof(SummaryInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ApiValidationError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Throttle(Name = "CreateUserThrottle", Seconds = 10)]
        public async Task<IActionResult> GetProfileSummary([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the review associated with a certain user profile.
        /// </summary>
        /// <param name="id">The id of the user to read the profile reviews.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("reviews")]
        [ProducesResponseType(typeof(List<ProfileReview>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ApiValidationError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Throttle(Name = "CreateUserThrottle", Seconds = 10)]
        public async Task<IActionResult> GetReviews([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the events associated with a certain user profile.
        /// </summary>
        /// <param name="id">The id of the user to read the profile events.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("events")]
        [ProducesResponseType(typeof(List<EventSummary>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ApiValidationError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Throttle(Name = "CreateUserThrottle", Seconds = 10)]
        public async Task<IActionResult> GetEvents([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
