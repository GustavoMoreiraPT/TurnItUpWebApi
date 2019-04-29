using Application.Dto.Tracks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITrackService
    {
        Task UploadTrack(int customerId, Track track);
    }
}
