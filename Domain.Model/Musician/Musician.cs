using System;
using System.Collections.Generic;
using Domain.Model.Users;
using Domain.Model.ValueObjects;

namespace Domain.Model.Musician
{
	public class Musician : TurnItUpUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string ArtisticName { get; set; }

		public string PhotoUrl { get; set; }

		public Location Location { get; set; }

		public Price Price { get; set; }

		public Age Age { get; set; }

		public Rating Rating { get; set; }

		public List<Gender> Genders { get; set; }

		public string Details { get; set; }

		public int ReviewsCount { get; }

		public void SetReviewsCount(int reviewsCount)
		{
			throw new NotImplementedException();
		}

		public void ClearGenders()
		{
			this.Genders.Clear();
		}
	}
}
