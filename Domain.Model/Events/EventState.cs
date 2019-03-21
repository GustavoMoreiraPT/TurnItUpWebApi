using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Events
{
	public class EventState
	{
		public int Id { get; set; }

		public string State { get; set; }

        public EventState(string stateName)
        {
            this.State = "Scheduled";
        }

        public EventState()
        {

        }
	}
}
