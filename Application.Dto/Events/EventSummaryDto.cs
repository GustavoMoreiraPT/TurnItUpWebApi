using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Events
{
	public class EventSummaryDto
	{
		public string EventName { get; set; }
		
		public string CreatorName { get; set; }

		public string MusicianName { get; set; }

		public DateTime Day { get; set; }

		public string Hours { get; set; }

		public string DurationTime { get; set; }

		public string Location { get; set; }

		public string Price { get; set; }

		public string Role { get; set; }

		public decimal Rating { get; set; }
	}
}
