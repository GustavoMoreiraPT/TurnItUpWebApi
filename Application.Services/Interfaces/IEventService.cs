using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Events;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
	public interface IEventService
	{
		Task CreateEvent(CreateEventDto eventDto);
		List<EventSummaryDto> GetEvents();
	}
}
