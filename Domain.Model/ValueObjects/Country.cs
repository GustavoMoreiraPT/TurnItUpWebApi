using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Country
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<City> Cities { get; set; }
	}
}
