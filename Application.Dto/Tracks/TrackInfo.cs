﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks
{
    public class TrackInfo
    {
        public Photo Photo { get; set; }

        public int TrackId { get; set; }

        public string Title { get; set; }

        public string ArtistName { get; set; }

        public int TrackDurationTime { get; set; }

        public int LikesCount { get; set; }

        public int PlaysCount { get; set; }

        public string TrackPhotoLocation { get; set; }

        public string TrackAudioLocation { get; set; }
    }
}
