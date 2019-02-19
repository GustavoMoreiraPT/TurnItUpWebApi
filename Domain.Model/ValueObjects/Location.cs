using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Location
	{
		public int Id { get; set; }

		public City City { get; set; }

		public Country Country { get; set; }
	}
}
