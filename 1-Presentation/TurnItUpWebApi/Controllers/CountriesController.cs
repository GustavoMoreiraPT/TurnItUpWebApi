using Application.Dto.Countries;
using Application.Dto.QueryParams;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TurnItUpWebApi.Filters;
using TurnItUpWebApi.ResponseModels;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/countries")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        /// <summary>
        /// Gets all the existing countries
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ApiValidationError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Policy = "ApiUser")]
        [Throttle(Name = "CountriesThrottle", Seconds = 5)]
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

            var countries = this.countriesService.GettAllCountries(language.Code);

            return Ok(countries);
        }
    }
}
