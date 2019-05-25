using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks
{
    public class TrackPlayRequest
    {
        public int TrackId { get; set; }

        public Guid AccountId { get; set; }

        public DateTime Date { get; set; }
    }
}
