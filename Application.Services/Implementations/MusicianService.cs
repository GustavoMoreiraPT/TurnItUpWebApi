using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Dto.Musicians;
using Application.Services.Interfaces;
using Application.Services.Mappings;
using Data.Repository.Configuration;
using Domain.Model;
using Domain.Model.Musician;
using Domain.Model.Users;

namespace Application.Services.Implementations
{
	public class MusicianService : IMusicianService
	{
		private readonly ApplicationDbContext context;

		public MusicianService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public async Task<MusicianAboutDto> CreateMusician(Customer customer)
		{
			var newMusician = new Musician
			{

				CustomerId = customer.Id,
			};

			this.context.TurnItUpUsers.Add(newMusician);
			await this.context.SaveChangesAsync();

			return null;
		}

		public async Task<MusicianAboutDto> CreateOrUpdateMusicianDetails(MusicianAboutDto musicianAboutDto)
		{
			var musician = await this.context.Musicians.FindAsync(musicianAboutDto.Id).ConfigureAwait(false);

			if (musician == null)
			{
				throw new ArgumentException($"There is no musician created with the id: {musicianAboutDto.Id}");
			}

			musician.AdaptFromAboutDto(musicianAboutDto);

			musician.Age = this.context.Ages.FirstOrDefault(x => x.Value == musician.Age.Value);
			musician.Price = this.context.Prices.FirstOrDefault(x => x.Value == musician.Price.Value);
			musician.Rating = this.context.Ratings.FirstOrDefault(x => x.Value == musician.Rating.Value);
			musician.Location = this.context.Locations.FirstOrDefault(x => x.City.Name == musicianAboutDto.City);
			musician.FirstName = musicianAboutDto.FirstName;


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
