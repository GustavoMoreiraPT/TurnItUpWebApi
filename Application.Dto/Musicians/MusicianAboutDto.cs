using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Musicians
{
	public class MusicianAboutDto
	{
		public int Id { get; set; }

		public string ArtisticName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		public decimal Price { get; set; }

		public List<GenderDto> Genders { get; set; }

		public string Country { get; set; }

		public string City { get; set; }

		public string Details { get; set; }

		public decimal Rating { get; set; }

		public string PhotoUrl { get; set; }

		public int ReviewsCount { get; set; }
	}
}
