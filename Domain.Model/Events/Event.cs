using Domain.Model.Reviews;
using Domain.Model.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Events
{
    public class Event
    {
        public int Id { get; set; }

        public int MusicianId { get; set; }

        public int EventManagerId { get; set; }

        public string Name { get; set; }

        public int RoleGroupId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public Location Location { get; set; }

        public ICollection<EventReview> Reviews { get; set; }
    }
}
