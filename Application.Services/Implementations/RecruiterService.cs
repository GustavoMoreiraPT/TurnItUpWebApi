using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Dto.Recruiters;
using Application.Services.Interfaces;
using Application.Services.Mappings;
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

		public async Task<RecruiterAboutDto> CreateOrUpdateRecruiterDetails(RecruiterAboutDto musicianAboutDto, List<Claim> claims)
		{
			var recruiterEmail = claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value;

			var identityUser = await this.usersService.FindByEmailAsync(recruiterEmail).ConfigureAwait(false);

			if (identityUser == null)
			{
				throw new ArgumentException("identity user not found for this musician");
			}

			var customer = this.context.Customers.FirstOrDefault(x => x.IdentityId == identityUser.Id);

			if (customer == null)
			{
				throw new ArgumentException("Customer not valid for this identity user.");
			}

			var recruiter = await this.context.Recruiters.FindAsync(musicianAboutDto.Id).ConfigureAwait(false);

			if (recruiter == null)
			{
				throw new ArgumentException($"There is no musician created with the id: {musicianAboutDto.Id}");
			}

			if (recruiter.CustomerId != customer.Id)
			{
				throw new ArgumentException("Only the owner of the acconunt can change his profile!");
			}

			recruiter.AdaptFromAboutDto(musicianAboutDto);

			recruiter.Price = this.context.Prices.FirstOrDefault(x => x.Value == recruiter.Price.Value);
			recruiter.Rating = this.context.Ratings.FirstOrDefault(x => x.Value == recruiter.Rating.Value);
			recruiter.Location = this.context.Locations.FirstOrDefault(x => x.City.Name == musicianAboutDto.City);

			this.context.Recruiters.Update(recruiter);

			await this.context.SaveChangesAsync().ConfigureAwait(false);

			return recruiter.ToAboutDto();
		}
	}
}
