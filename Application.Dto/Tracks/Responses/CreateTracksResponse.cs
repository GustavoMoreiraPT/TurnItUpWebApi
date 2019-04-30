using Infrastructure.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks.Responses
{
    public class CreateTracksResponse
    {
        public List<Error> Errors { get; set; }
    }
}
