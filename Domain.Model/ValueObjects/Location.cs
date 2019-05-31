using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Location
	{
		public int Id { get; set; }

		public string City { get; set; }

		public int CountryGroupId { get; set; }
	}
}