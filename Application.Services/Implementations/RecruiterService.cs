using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Recruiters;
using Application.Services.Interfaces;
using Data.Repository.Configuration;

namespace Application.Services.Implementations
{
    public class RecruiterService : IRecruiterService
    {
        private readonly ApplicationDbContext context;
        private readonly IUsersService usersService;

        public RecruiterService(ApplicationDbContext context, IUsersService usersService)
        {
            this.context = context;
            this.usersService = usersService;
        }

        public Task<RecruiterAboutDto> CreateOrUpdateRecruiterDetails(RecruiterAboutDto musicianAboutDto, List<Claim> claims)
        {
            throw new System.NotImplementedException();
        }
    }
}
