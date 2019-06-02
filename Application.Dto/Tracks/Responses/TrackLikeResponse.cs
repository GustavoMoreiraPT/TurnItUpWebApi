using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks.Responses
{
    public class TrackLikeResponse
    {
        public bool TrackNotFound { get; set; }

        public bool TrackAlreadyLiked { get; set; }
    }
}
