//using Application.Dto.Recruiters;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;
//using Application.Services.Interfaces;
//using System.Collections.Generic;
//using System.Security.Claims;
//using System.Linq;

//namespace TurnItUpWebApi.Controllers
//{
//	[Route("v1/recruiters")]
//	public class RecruitersController : Controller
//	{
//		private readonly IRecruiterService recruitersService;

//		public RecruitersController(IRecruiterService recruiterService)
//		{
//			this.recruitersService = recruiterService;
//		}

//        [HttpPost]
//        [Route("{id}/about")]
//        [ProducesResponseType(200, Type = typeof(RecruiterAboutDto))]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(401)]
//        [ProducesResponseType(403)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(500)]
//        public async Task<IActionResult> CreateOrUpdateRecruiterDetailsAsync([FromRoute] int id, [FromBody] RecruiterAboutDto recruiterAboutDto)
//        {
//            if (id < 1)
//            {
//                return this.BadRequest("Recruiter ID can't be less than 1");
//            }

//            if (recruiterAboutDto == null)
//            {
//                return this.BadRequest("Recruiter body cannot be null");
//            }

//            if (id != recruiterAboutDto.Id)
//            {
//                return this.BadRequest("Id from route and body mismatch");
//            }

//            var identity = HttpContext.User.Identity as ClaimsIdentity;

//            if (identity != null)
//            {
//	            List<Claim> claims = identity.Claims.ToList();
//	            return this.Ok(await this.recruitersService.CreateOrUpdateRecruiterDetails(recruiterAboutDto, claims).ConfigureAwait(false));
//            }

//            return this.BadRequest();
//        }

//        [HttpGet]
//		public async Task<IActionResult> GetAllAsync()
//		{
//			throw new NotImplementedException();
//		}

//		[HttpGet]
//		[Route("{id}")]
//		public async Task<IActionResult> GetAsync()
//		{
//			throw new NotImplementedException();
//		}

//		[HttpPost]
//		[Route("{id}/offers")]
//		public async Task<IActionResult> CreateOfferAsync()
//		{
//			throw new NotImplementedException();
//		}

//		[HttpPut]
//		[Route("{id}")]
//		public async Task<IActionResult> UpdateAsync()
//		{
//			throw new NotImplementedException();
//		}
//	}
//}
