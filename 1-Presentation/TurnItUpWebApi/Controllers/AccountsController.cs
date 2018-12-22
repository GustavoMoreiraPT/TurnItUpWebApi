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
        public async Task<IActionResult> CreateAccount()
        {
            throw new NotImplementedException();
        }
    }
}
