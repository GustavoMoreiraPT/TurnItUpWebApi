using Application.Dto.QueryParams;
using Application.Dto.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/roles")]
    public class RolesController : Controller
    {
        public RolesController()
        {

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] Language language)
        {
            throw new NotImplementedException();
        }
    }
}
