using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto.Musicians;
using Domain.Model.Musician;
using Domain.Model.ValueObjects;

namespace Application.Services.Mappings
{
	public static class MusicianExtension
	{
		public static MusicianAboutDto ToAboutDto(this Musician musician)
		{
			return new MusicianAboutDto
			{
				ArtisticName = musician.ArtisticName,
				City = musician.Location?.City?.Name,
				Country = musician.Location?.Country?.Name,
				Details = musician.Details,
				FirstName = musician.FirstName,
				Genders = GetGenders(musician.Genders),
				Id = musician.Id,
				PhotoUrl = musician.PhotoUrl,
				Price = musician.Price == null ? 0 : musician.Price.Value,
				Rating = musician.Rating.GetValue(),
				ReviewsCount = musician.ReviewsCount
			};
		}

		private static List<string> GetGenders(List<Gender> genders)
		{
			return genders.Select(x => x.Name).ToList();
		}
	}
}
