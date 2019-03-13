using Application.Dto.Events;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TurnItUpWebApi.Controllers
{
	[Route("v1/events")]
	public class EventsController : Controller
	{
		private readonly IEventService eventService;
		
		public EventsController(IEventService eventService)
		{
			this.eventService = eventService;
		}

		[HttpPost]
		[Authorize(Policy = "ApiUser")]
		public async Task<IActionResult> CreateEvent([FromBody]CreateEventDto eventDto)
		{
			if (eventDto == null)
			{
				return this.BadRequest();
			}

			await this.eventService.CreateEvent(eventDto).ConfigureAwait(false);

			return this.Ok();
		}

		[HttpGet]
		[Authorize(Policy = "ApiUser")]
		public async Task<IActionResult> GetEvents()
		{
			return Ok(this.eventService.GetEvents());
		}
	}
}
