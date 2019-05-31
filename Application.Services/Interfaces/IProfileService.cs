using Application.Dto.Profile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IProfileService
    {
        Task<SummaryInfo> GetSummaryInfo(Guid accountId, string languageCode);
    }
}
