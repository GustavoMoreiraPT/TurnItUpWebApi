using Domain.Model.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Tracks
{
    public class Track
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public int DurationInSeconds { get; set; }

        public Image TrackPhoto { get; set; }

        public ICollection<TrackPlay> Plays { get; set; }

        public ICollection<TrackLike> Likes { get; set; }
    }
}
