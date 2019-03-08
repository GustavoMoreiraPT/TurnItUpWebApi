using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model.ValueObjects;

namespace Domain.Model.Events
{
	public class EventLocation
	{
		public int Id { get; set; }

		public string StreetName { get; set; }

		public int StreetNumber { get; set; }

		public Location Location { get; set; }
	}
}
