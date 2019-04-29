using Application.Dto.QueryParams;
using Application.Dto.Users;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/genres")]
    public class GenresController : Controller
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GenreDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        public IActionResult GetAll([FromQuery] Language language)
        {
            if (language == null)
            {
                return this.BadRequest("Language query parameter must be provided");
            }

            if (string.IsNullOrWhiteSpace(language.Code))
            {
                language.Code = "en";
            }

            var genres = this.genresService.GettAllGenres(language.Code);

            return Ok(genres);
        }
    }
}
