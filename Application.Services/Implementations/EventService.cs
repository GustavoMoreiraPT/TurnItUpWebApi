using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Events;
using Application.Services.Interfaces;
using Application.Services.Mappings;
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

		public async Task CreateEvent(CreateEventDto eventDto)
		{
			var newEvent = new Event();

			var recruiter = this.context.Recruiters.FirstOrDefault(x => x.Name == eventDto.RecruiterEmail);

			if (recruiter == null)
			{
				throw new ArgumentException();
			}

			var location = this.context.Locations.FirstOrDefault(x =>
				x.City.Name == eventDto.CityName && x.Country.Name == eventDto.CountryName);

			if (location == null)
			{
				throw new ArgumentException();
			}

			var price = this.context.Prices.FirstOrDefault(x => x.Value == eventDto.Price);

			if (price == null)
			{
				throw new ArgumentException();
			}

			var role = this.context.Role.FirstOrDefault(x => x.Name == eventDto.RoleName);

			if (role == null)
			{
				throw new ArgumentException();
			}

            newEvent.State = new EventState("Scheduled");
            newEvent.Name = eventDto.Name;
            newEvent.CreatorId = recruiter.Id;
            newEvent.Location = new EventLocation
            {
                Location = location,
                StreetName = eventDto.Location,
                StreetNumber = eventDto.LocationNumber,
            };
            newEvent.StartTime = eventDto.Date;
            TimeSpan startingHour = new TimeSpan((int)eventDto.StartingHour, 0, 0);
            newEvent.StartTime = newEvent.StartTime + startingHour;
            TimeSpan endingHour = 
                new TimeSpan((int)eventDto.StartingHour + (int)eventDto.DurationInHours, 0, 0);
            newEvent.EndTime = eventDto.Date;
            newEvent.EndTime = newEvent.EndTime + endingHour;

            newEvent.Price = this.context.Prices.FirstOrDefault(x => x.Value == eventDto.Price);
            newEvent.Role = this.context.Role.FirstOrDefault(x => x.Name == eventDto.RoleName);
            newEvent.RoleId = newEvent.Role.Id;

            this.context.Events.Add(newEvent);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
		}

		public List<EventSummaryDto> GetEvents()
		{
			var events = this.context.Events.ToList();

			return events.ToSummaryDtos();
		}
	}
}
