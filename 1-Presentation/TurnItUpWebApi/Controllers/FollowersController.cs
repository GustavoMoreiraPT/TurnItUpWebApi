using Application.Dto.Followers.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TurnItUpWebApi.Filters;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/followers")]
    public class FollowersController : Controller
    {
        private readonly IFollowersService followersService;

        public FollowersController(IFollowersService followersService)
        {
            this.followersService = followersService;
        }

        /// <summary>
        ///  The user from HttpContext will be added as a followers for the chosen account.
        /// </summary>
        /// <param name="request"> The request to perform the follow action. CustomerToFollow: The customer
        /// which will be added as a follower. CustomerToBeFollowed: The customer which will have one 
        /// more follower after a successfull request.</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
        public async Task<IActionResult> UploadFile([FromBody] AddFollowerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
