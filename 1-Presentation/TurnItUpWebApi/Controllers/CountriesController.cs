using Application.Dto.Countries;
using Application.Dto.QueryParams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurnItUpWebApi.ResponseModels;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/countries")]
    public class CountriesController : Controller
    {
        public CountriesController()
        {

        }

        /// <summary>
        /// Gets all the existing countries
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ApiValidationError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] Language language)
        {
            throw new NotImplementedException();
        }
    }
}
