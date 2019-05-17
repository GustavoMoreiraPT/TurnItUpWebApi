using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto.Events;
using Application.Services.Interfaces;
using Data.Repository.Configuration;

namespace Application.Services.Implementations
{
	public class EventService : IEventService
	{
		private readonly ApplicationDbContext context;


		public EventService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task CreateEvent(CreateEventDto eventDto)
		{
            throw new NotImplementedException();
        }

		public EventSummaryDto GetEvent(int eventId)
		{
            throw new NotImplementedException();
		}

		public List<EventSummaryDto> GetEvents()
		{
            throw new NotImplementedException();
        }
	}
}
