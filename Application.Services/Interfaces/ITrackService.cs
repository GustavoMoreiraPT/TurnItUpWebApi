using Application.Dto.Tracks;
using Application.Dto.Tracks.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITrackService
    {
        Task<CreateTracksResponse> UploadTrack(int customerId, Track track);
    }
}
