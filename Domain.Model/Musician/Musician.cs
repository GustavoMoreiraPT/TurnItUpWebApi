using System;
using System.Collections.Generic;
using Domain.Model.Users;
using Domain.Model.ValueObjects;

namespace Domain.Model.Musician
{
	public class Musician : TurnItUpUser
	{
		public string FirstName { get; set; }

		public string ArtisticName { get; set; }

		public Age Age { get; set; }

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
