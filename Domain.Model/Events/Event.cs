using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model.Musician;
using Domain.Model.ValueObjects;

namespace Domain.Model.Events
{
	public class Event
	{
		public int Id { get; set; }

		public int MusicianId { get; set; }

		public int CreatorId { get; set; }

		public int RoleId { get; set; }

		public string Name { get; set; }
		
		public DateTime EndTime { get; set; }

		public DateTime StartTime { get; set; }

		public Price Price { get; set; }

		public EventLocation Location { get; set; }

		public Rating Rating { get; set; }

		public EventState State { get; set; }

		public virtual  Musician.Musician Musician { get; set; }

		public virtual Recruiter.Recruiter Recruiter { get; set; }

		public virtual Role Role { get; set; }
	}
}
