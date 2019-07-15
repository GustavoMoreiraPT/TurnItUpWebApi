using Infrastructure.CrossCutting.Helpers;
using System.Collections.Generic;

namespace Application.Dto.Tracks.Responses
{
    public class CreateTracksResponse
    {
        public int TrackId { get; set; }

        public string TrackAudioLocation { get; set; }

        public List<Error> Errors { get; set; }
    }
}
