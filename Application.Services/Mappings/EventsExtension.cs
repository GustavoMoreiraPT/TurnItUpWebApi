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
				Hours = e.StartTime.Hour.ToString(),
                Location = e.Location.StreetNumber + ',' + e.Location.StreetName,
                MusicianName = e.Musician.ArtisticName,
                Price = e.Price.Value.ToString(),
                Rating = e.Rating.Value,
                Role = e.Role.Name
			}).ToList();
		}

		public static EventSummaryDto ToSummaryDto(this Event musicEvent)
		{
			return new EventSummaryDto
			{
				EventName = musicEvent.Name,
				CreatorName = musicEvent.Recruiter.Name,
				Day = musicEvent.StartTime.Date,
				DurationTime = (musicEvent.EndTime - musicEvent.StartTime).Hours.ToString(),
				Hours = musicEvent.StartTime.Hour.ToString(),
				Location = musicEvent.Location.StreetNumber + ',' + musicEvent.Location.StreetName,
				MusicianName = musicEvent.Musician.ArtisticName,
				Price = musicEvent.Price.Value.ToString(),
				Rating = musicEvent.Rating.Value,
				Role = musicEvent.Role.Name
			};
		}
	}
}
