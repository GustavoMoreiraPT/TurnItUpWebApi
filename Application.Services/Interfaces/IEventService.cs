using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dto.Events;

namespace Application.Services.Interfaces
{
	public interface IEventService
	{
		Task CreateEvent(CreateEventDto eventDto);

		List<EventSummaryDto> GetEvents();

		EventSummaryDto GetEvent(int eventId);
	}
}
