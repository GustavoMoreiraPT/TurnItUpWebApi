using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks
{
    public class CreateTrackRequest
    {
        public string ArtistName { get; set; }

        public string TrackName { get; set; }

        public IFormFile Track { get; set; }
    }
}
