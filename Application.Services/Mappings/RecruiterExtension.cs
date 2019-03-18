using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Dto;
using Application.Dto.Recruiters;
using Domain.Model.Recruiter;
using Domain.Model.ValueObjects;

namespace Application.Services.Mappings
{
	public static class RecruiterExtension
	{
		public static RecruiterAboutDto ToAboutDto(this Recruiter recruiter)
		{
			return new RecruiterAboutDto
			{
				City = recruiter.Location?.City?.Name,
				Country = recruiter.Location?.Country?.Name,
				Details = recruiter.Details,
				FirstName = recruiter.Name,
				Genders = GetGenders(recruiter.Genders),
				Id = recruiter.Id,
				PhotoUrl = recruiter.PhotoUrl,
				Price = recruiter.Price == null ? 0 : recruiter.Price.Value,
				Rating = recruiter.Rating.GetValue(),
				ReviewsCount = recruiter.ReviewsCount
			};
		}

		public static Recruiter AdaptFromAboutDto(this Recruiter recruiter, RecruiterAboutDto details)
		{
			recruiter.Price = AdaptPrice(recruiter.Price, details.Price);
			recruiter.Rating = AdaptRating(recruiter.Rating, details.Rating);
			recruiter.Details = details.Details;
			recruiter.Name = details.FirstName + details.LastName;
			recruiter.LastName = details.LastName;
			recruiter.Genders = AdaptGenders(recruiter.Genders, details.Genders);

			return recruiter;
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
				gendersToUpdate.Add(new Gender { Id = item.Id, Name = item.Name });
			}

			return gendersToUpdate;
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
			return genders.Select(x => new GenderDto { Id = x.Id, Name = x.Name }).ToList();
		}
	}
}
