using Domain.Model.Users;
using Domain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class TurnItUpUser
	{
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public string Email { get; set; }

		public string LastName { get; set; }

		public string PhotoUrl { get; set; }

		public Location Location { get; set; }

		public Price Price { get; set; }

		public Rating Rating { get; set; }

		public List<Gender> Genders { get; set; }

		public string Details { get; set; }

		public int ReviewsCount { get; set; }

		public virtual Customer Customer { get; set; }
	}
}
