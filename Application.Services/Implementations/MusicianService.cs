using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Musicians;
using Application.Services.Interfaces;
using Application.Services.Mappings;
using Data.Repository.Configuration;
using Domain.Model;
using Domain.Model.Musician;
using Domain.Model.Users;
using Microsoft.AspNetCore.Authorization.Infrastructure;

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
			var musicianEmail = musicianClaims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;

			var identityUser = await this.usersService.FindByEmailAsync(musicianEmail).ConfigureAwait(false);

			if (identityUser == null)
			{
				throw new ArgumentException("identity user not found for this musician");
			}

			var customer = this.context.Customers.FirstOrDefault(x => x.IdentityId == identityUser.Id);

			if (customer == null)
			{
				throw new ArgumentException("Customer not valid for this identity user.");
			}

			var musician = await this.context.Musicians.FindAsync(musicianAboutDto.Id).ConfigureAwait(false);

			if (musician == null)
			{
				throw new ArgumentException($"There is no musician created with the id: {musicianAboutDto.Id}");
			}

			if (musician.CustomerId != customer.Id)
			{
				throw new ArgumentException("Only the owner of the acconunt can change his profile!");
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
