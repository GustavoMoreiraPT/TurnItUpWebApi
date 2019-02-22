using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Musicians;

namespace Application.Services.Interfaces
{
	public interface IMusicianService
	{
		Task<MusicianAboutDto> GetMusicianDetails(int musicianId);

		Task<MusicianAboutDto> CreateMusicianDetails(MusicianAboutDto musicianAboutDto);
	}
}
