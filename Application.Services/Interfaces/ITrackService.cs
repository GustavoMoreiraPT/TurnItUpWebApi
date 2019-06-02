﻿using Application.Dto.Tracks;
using Application.Dto.Tracks.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITrackService
    {
        Task<CreateTracksResponse> UploadTrack(Guid customerId, IFormFile track);

        Task<List<TrackInfo>> GetTracksInfo(Guid customerId);

        Task<bool> DeleteTrack(Guid customerId, int trackId);
    }
}
