using Application.Dto.Recruiters;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IRecruiterService
    {
        Task<RecruiterAboutDto> CreateOrUpdateRecruiterDetails(RecruiterAboutDto musicianAboutDto, List<Claim> claims);
    }
}
