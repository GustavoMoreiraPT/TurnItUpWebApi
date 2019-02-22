using System;
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

		public MusicianService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<MusicianAboutDto> CreateMusicianDetails(MusicianAboutDto musicianAboutDto)
		{
			var musician = await this.context.Musicians.FindAsync(musicianAboutDto.Id).ConfigureAwait(false);

			if (musician == null)
			{
				throw new ArgumentException($"There is no musician created with the id: {musicianAboutDto.Id}");
			}

			musician.AdaptFromAboutDto(musicianAboutDto);

			this.context.Musicians.Update(musician);

			await this.context.SaveChangesAsync().ConfigureAwait(false);

			return musician.ToAboutDto();
		}

		public async Task<MusicianAboutDto> GetMusicianDetails(int musicianId)
		{
			var musician = await this.context.Musicians.FindAsync(musicianId).ConfigureAwait(false);

			if (musician == null)
			{
				throw new ArgumentException($"Musican with id: {musicianId} does not exist");
			}

			return musician.ToAboutDto();
		}
	}
}
