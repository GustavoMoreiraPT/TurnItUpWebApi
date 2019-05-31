using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Tracks
{
    public class TrackPlay
    {
        public int Id { get; set; }

        public int TrackId { get; set; }

        public int AccountId { get; set; }

        public DateTime Date { get; set; }
    }
}
