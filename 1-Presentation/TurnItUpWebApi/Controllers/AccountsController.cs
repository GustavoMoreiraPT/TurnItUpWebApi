using Application.Requests.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/accounts")]
    [ApiController]
    public class AccountsController
    {
        public AccountsController()
        {

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAccount([FromBody]NewAccountRequest newAccountRequest)
        {
            throw new NotImplementedException();
        }
    }
}
