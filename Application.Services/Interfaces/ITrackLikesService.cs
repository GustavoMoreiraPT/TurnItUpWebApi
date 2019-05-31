using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITrackLikesService
    {
        Task LikeTrack(Guid accountId, int trackId);
    }
}
