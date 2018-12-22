using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
    [Route("v1/recruiters")]
    [ApiController]
    public class RecruitersController
    {
        public RecruitersController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("{id}/offers")]
        public async Task<IActionResult> CreateOfferAsync()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
