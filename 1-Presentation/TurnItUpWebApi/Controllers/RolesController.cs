using Application.Dto.QueryParams;
using Application.Dto.Users;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RoleDto>), StatusCodes.Status200OK)]
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

            var roles = this.rolesService.GetAllRoles(language.Code);

            return Ok(roles);
        }
    }
}
