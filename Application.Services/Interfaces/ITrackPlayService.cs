﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITrackPlayService
    {
        Task PlayTrack(Guid accountId, int trackId);
    }
}
