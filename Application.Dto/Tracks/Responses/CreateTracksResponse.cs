using Infrastructure.CrossCutting.Helpers;
using System.Collections.Generic;

namespace Application.Dto.Tracks.Responses
{
    public class CreateTracksResponse
    {
        public List<Error> Errors { get; set; }
    }
}
