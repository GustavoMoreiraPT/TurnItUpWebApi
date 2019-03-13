using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto.Events;
using Domain.Model.Events;

namespace Application.Services.Mappings
{
	public static class EventsExtension
	{
		public static List<EventSummaryDto> ToSummaryDtos(this List<Event> events)
		{
			return events.Select(e => new EventSummaryDto
			{
			    EventName = e.Name,
				CreatorName = e.Recruiter.Name,
				Day = e.StartTime.Date,
				DurationTime = (e.EndTime - e.StartTime).Hours.ToString(),
				Hours = e.StartTime.Hour.ToString()
			}).ToList();
		}
	}
}
