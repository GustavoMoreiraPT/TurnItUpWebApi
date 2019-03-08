using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Events;

namespace Application.Services.Interfaces
{
	public interface IEventService
	{
		Task CreateEvent(CreateEventDto eventDto);
	}
}
