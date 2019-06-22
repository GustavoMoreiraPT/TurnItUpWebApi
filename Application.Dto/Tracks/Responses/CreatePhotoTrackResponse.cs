using Infrastructure.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Tracks.Responses
{
    public class CreatePhotoTrackResponse
    {
        public int PhotoId { get; set; }

        public List<Error> Errors { get; set; }
    }
}
