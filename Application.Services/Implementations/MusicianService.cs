using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Musicians;
using Application.Services.Interfaces;
using Application.Services.Mappings;
using Data.Repository.Configuration;

namespace Application.Services.Implementations
{
	public class MusicianService : IMusicianService
	{
		private readonly ApplicationDbContext context;
		private readonly IUsersService usersService;

		public MusicianService(
			ApplicationDbContext context,
			IUsersService usersService
			)
		{
			this.context = context;
			this.usersService = usersService;
		}

		public async Task<MusicianAboutDto> CreateOrUpdateMusicianDetails(MusicianAboutDto musicianAboutDto, List<Claim> musicianClaims)
		{
            throw new NotImplementedException();
		}

		public async Task<MusicianAboutDto> GetMusicianDetails(int musicianId)
		{
            throw new NotImplementedException();
		}
	}
}
