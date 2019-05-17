using System;

namespace Application.Dto.Events
{
	public class CreateEventDto
	{
		public string Name { get; set; }

		public string RecruiterEmail { get; set; }

		public DateTime Date { get; set; }

		public double DurationInHours { get; set; }

		public double StartingHour { get; set; }

		public decimal Price { get; set; }

		public string Location { get; set; }

        public int LocationNumber { get; set; }

		public string CityName { get; set; }

		public string CountryName { get; set; }

		public string RoleName { get; set; }
	}
}
