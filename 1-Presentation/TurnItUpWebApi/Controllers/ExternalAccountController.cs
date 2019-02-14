using Application.Dto.Users;
using Application.Services.Interfaces;
using Domain.Model.Users;
using Infrastructure.CrossCutting.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Settings;
using static Infrastructure.CrossCutting.Helpers.FacebookApiResponses;
using Microsoft.Extensions.Options;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts/external")]
    public class ExternalAccountController : Controller
    {
        private readonly IUsersService userService;
        private static readonly HttpClient Client = new HttpClient();
        private readonly FacebookAuthSettings _fbAuthSettings;

        public ExternalAccountController(IUsersService usersService, IOptions<FacebookAuthSettings> fbAuthSettingsAccessor)
        {
            this.userService = usersService;
            this._fbAuthSettings = fbAuthSettingsAccessor.Value;
        }

        // POST api/externalauth/facebook
        [HttpPost]
        [Route("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookUserDto model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await this.userService.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new AppUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await this.userService.CreateUserAsync(appUser, userInfo, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
            }

            // generate the jwt for the local user...
            var localUser = await this.userService.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
            }

            var claims = await this.userService.GetClaimsIdentity(localUser.UserName, localUser.Id);
            var jwt = await this.userService.GenerateToken(claims, localUser.UserName, localUser.PasswordHash, string.Empty, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
        }
    }
}

