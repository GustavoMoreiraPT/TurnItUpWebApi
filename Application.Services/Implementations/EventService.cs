using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Events;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using Domain.Model.Events;

namespace Application.Services.Implementations
{
	public class EventService : IEventService
	{
		private readonly ApplicationDbContext context;


		public EventService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public Task CreateEvent(CreateEventDto eventDto)
		{
			var newEvent = new Event();
			return null;
		}
	}
}
