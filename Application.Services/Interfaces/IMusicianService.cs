using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Musicians;
using Domain.Model.Users;

namespace Application.Services.Interfaces
{
	public interface IMusicianService
	{

		Task<MusicianAboutDto> CreateMusician(Customer customer);

		Task<MusicianAboutDto> GetMusicianDetails(int musicianId);

		Task<MusicianAboutDto> CreateOrUpdateMusicianDetails(MusicianAboutDto musicianAboutDto, List<Claim> claims);
	}
}
