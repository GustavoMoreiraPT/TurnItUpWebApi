using Domain.Model.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Tracks
{
    public class Track
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string Extension { get; set; }

        public string TrackName { get; set; }

        public string ArtistName { get; set; }

        public int DurationInSeconds { get; set; }

        public string TrackAudioLocation { get; set; }

        public string TrackPhotoLocation { get; set; }

        public Image TrackPhoto { get; set; }

        public ICollection<TrackPlay> Plays { get; set; }

        public ICollection<TrackLike> Likes { get; set; }
    }
}
