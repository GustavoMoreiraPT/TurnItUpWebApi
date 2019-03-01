using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Application.Dto;
using Application.Dto.Musicians;
using Domain.Model.Musician;
using Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage;

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
				Age =  musician.Age.Value,
				PhotoUrl = musician.PhotoUrl,
				Price = musician.Price == null ? 0 : musician.Price.Value,
				Rating = musician.Rating.GetValue(),
				ReviewsCount = musician.ReviewsCount
			};
		}

		public static Musician AdaptFromAboutDto(this Musician musician, MusicianAboutDto details)
		{
			musician.Price = AdaptPrice(musician.Price, details.Price);
			musician.Age = AdaptAge(musician.Age, details.Age);
			musician.Rating = AdaptRating(musician.Rating, details.Rating);
			musician.ArtisticName = details.ArtisticName;
			musician.Details = details.Details;
			musician.FirstName = details.FirstName;
			musician.LastName = details.LastName;
			musician.Genders = AdaptGenders(musician.Genders, details.Genders);

			return musician;
		}

		private static Rating AdaptRating(Rating ratingToUpdate, decimal currentRating)
		{
			if (ratingToUpdate == null)
			{
				ratingToUpdate = new Rating();
				ratingToUpdate.Value = currentRating;
				return ratingToUpdate;
			}

			ratingToUpdate.Value = currentRating;
			return ratingToUpdate;
		}

		private static List<Gender> AdaptGenders(List<Gender> gendersToUpdate, List<GenderDto> currentGenders)
		{
			if (gendersToUpdate == null)
			{
				gendersToUpdate = new List<Gender>();
			}

			gendersToUpdate.Clear();

			foreach (var item in currentGenders)
			{
				gendersToUpdate.Add(new Gender {Id = item.Id, Name = item.Name });
			}

			return gendersToUpdate;
		}

		private static Age AdaptAge(Age ageToUpdate, int currentAge)
		{
			if (ageToUpdate == null)
			{
				ageToUpdate = new Age();
				ageToUpdate.Value = currentAge;
				return ageToUpdate;
			}

			ageToUpdate.SetAge(currentAge);
			return ageToUpdate;
		}

		private static Price AdaptPrice(Price priceToUpdate, decimal currentPrice)
		{
			if (priceToUpdate == null)
			{
				priceToUpdate = new Price(currentPrice);
				return priceToUpdate;
			}

			priceToUpdate.SetPrice(currentPrice);
			return priceToUpdate;
		}

		private static List<GenderDto> GetGenders(List<Gender> genders)
		{
			return genders.Select(x => new GenderDto {Id = x.Id, Name = x.Name}).ToList();
		}
	}
}
