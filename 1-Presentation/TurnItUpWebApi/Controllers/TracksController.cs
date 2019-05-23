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
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "TracksThrottle", Seconds = 10)]
        [DisableFormValueModelBinding]
        //[ValidateAntiForgeryToken]
        // 1. Disable the form value model binding here to take control of handling 
        //    potentially large files.
        // 2. Typically antiforgery tokens are sent in request body, but since we 
        //    do not want to read the request body early, the tokens are made to be 
        //    sent via headers. The antiforgery token filter first looks for tokens
        //    in the request header and then falls back to reading the body.
        public async Task<IActionResult> UploadFileTest([FromRoute] Guid id)
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
            }
                // Used to accumulate all the form url encoded key value pairs in the 
                // request.
                var formAccumulator = new KeyValueAccumulator();
                string targetFilePath = null;

                var boundary = MultipartRequestHelper.GetBoundary(
                    //MediaTypeHeaderValue.Parse(Request.ContentType),
                    null,defaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, HttpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    ContentDispositionHeaderValue contentDisposition;
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            targetFilePath = Path.GetTempFileName();
                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);

                                //_logger.LogInformation($"Copied the uploaded file '{targetFilePath}'");
                            }
                        }
                        else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        {
                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = GetEncoding(section);
                            using (var streamReader = new StreamReader(
                                section.Body,
                                encoding,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                                if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = String.Empty;
                                }
                                formAccumulator.Append(key.ToString(), value);

                                if (formAccumulator.ValueCount > defaultFormOptions.ValueCountLimit)
                                {
                                    throw new InvalidDataException($"Form key count limit {defaultFormOptions.ValueCountLimit} exceeded.");
                                }
                            }
                        }
                    }
                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }


            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);



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
