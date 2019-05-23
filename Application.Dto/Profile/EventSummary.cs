using Application.Dto.Links;
using System;

namespace Application.Dto.Profile
{
    public class EventSummary
    {
        public EventLink EventLink { get; set; }

        public UserLink UserLink { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public decimal DurationInHours { get; set; }

        public int Price { get; set; }

        public string Role { get; set; }
    }
}
