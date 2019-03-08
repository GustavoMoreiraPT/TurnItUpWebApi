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

			return null;
		}
	}
}
