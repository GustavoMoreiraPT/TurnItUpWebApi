using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks
{
    public class TrackInfo
    {
        public Photo Photo { get; set; }

        public int TrackId { get; set; }

        public string Title { get; set; }

        public string TrackDurationTime { get; set; }

        public int Likes { get; set; }

        public int PlaysCount { get; set; }
    }
}
