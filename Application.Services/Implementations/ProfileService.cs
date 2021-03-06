﻿using Application.Dto.Profile;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using Domain.Model.Events;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public ProfileService(ApplicationDbContext context, UserManager<AppUser> userRepository)
        {
            this.context = context;
            this.userManager = userRepository;
        }

        public async Task<SummaryInfo> GetSummaryInfo(Guid accountId, string languageCode)
        {
            var identityUser = await this.userManager.FindByIdAsync(accountId.ToString());

            if (identityUser == null)
            {
                return null;
            }

            var customer = this.context.Customers
                .Include(x => x.Genders)
                .Include(x => x.Roles)
                .Include(x => x.HeaderPhoto)
                .Include(x => x.ProfilePhoto)
                .Include(x => x.Location)
                .Include(x => x.SocialNetworks)
                .FirstOrDefault(x => x.IdentityId == accountId.ToString());

            var profilePhotoFile = File.ReadAllBytes($"C:/TurnItUp/ProfilePhotos/{customer.Id}/{customer.ProfilePhoto.Name}.{customer.ProfilePhoto.Extension}");
            var coverPhotoFile = File.ReadAllBytes($"C:/TurnItUp/HeaderPhotos/{customer.Id}/{customer.HeaderPhoto.Name}.{customer.HeaderPhoto.Extension}");
            var languageRoles = this.context.LanguageRoles.Where(x => x.Language == languageCode);
            var languageGenres = this.context.LanguageGenres.Where(x => x.Language == languageCode);
            var languageCoutries = this.context.LanguageCountries.Where(x => x.Language == languageCode);

            var customerRoles = languageRoles.Where(x => customer.Roles.Select(y => y.GroupId).Contains(x.RoleGroupId));
            var customerGenres = languageGenres.Where(x => customer.Genders.Select(y => y.GroupId).Contains(x.LanguageGroupId));
            var customerCountry = languageCoutries.FirstOrDefault(x => customer.Location.CountryGroupId == x.CountryGroupId);

            var summaryInfo = new SummaryInfo();

            summaryInfo.ProfilePhoto = new Dto.Photo
            {
                Name = customer.ProfilePhoto.Name,
                Extension = customer.ProfilePhoto.Extension,
                Content = Convert.ToBase64String(profilePhotoFile)
            };

            summaryInfo.CoverPhoto = new Dto.Photo
            {
                Name = customer.HeaderPhoto.Name,
                Extension = customer.HeaderPhoto.Extension,
                Content = Convert.ToBase64String(coverPhotoFile)
            };

            summaryInfo.Name = customer.ProfileName;
            summaryInfo.Roles = customerRoles.Select(x => x.Name).ToList();
            summaryInfo.FollowersCount = customer.FollowersCount;
            summaryInfo.SocialMediaLinks = customer.SocialNetworks.Select(x => new Dto.SocialMedia.SocialNetwork
            {
                Name = x.Name,
                Url = x.Url
            }).ToList();

            summaryInfo.Rating = customer.Rating;
            summaryInfo.ReviewsCount = customer.ReviewsCount;
            summaryInfo.Price = customer.Price;
            summaryInfo.Genres = customerGenres.Select(x => x.Name).ToList();
            summaryInfo.Country = customerCountry.Name;
            summaryInfo.City = customer.Location.City;
            summaryInfo.Type = (Dto.Enum.AccountTypes)customer.CustomerType;

            return summaryInfo;
        }

        public async Task<List<EventSummary>> GetEventsSummary(Guid accountId, string languageCode)
        {
            var identityUser = await this.userManager.FindByIdAsync(accountId.ToString());

            if (identityUser == null)
            {
                return null;
            }

            var customer = this.context.Customers.FirstOrDefault(x => x.IdentityId == accountId.ToString());

            var customerEvents = new List<Event>();

            if (customer.CustomerType == CustomerType.Musician)
            {//get events by musician Id
                customerEvents = this.context.Events
                    .Include(x => x.Location)
                    .Where(x => x.MusicianId == customer.Id)
                    .ToList();
            }

            if (customer.CustomerType == CustomerType.EventManager)
            {//get events by event manager
                customerEvents = this.context.Events
                    .Include(x => x.Location)
                    .Where(x => x.EventManagerId == customer.Id)
                    .ToList();
            }

            var languageCoutries = this.context.LanguageCountries.Where(x => x.Language == languageCode);

            var languageRoles = this.context.LanguageRoles.Where(x => x.Language == languageCode);

            var eventSummaries = new List<EventSummary>();

            foreach (var item in customerEvents)
            {
                var summary = new EventSummary();

                var eventCountry = languageCoutries.FirstOrDefault(x => x.CountryGroupId == item.Location.CountryGroupId).Name;

                var creator = this.context.Customers.FirstOrDefault(x => x.Id == customer.Id);

                if (creator == null)
                {
                    continue;
                }

                summary.EventLink = new Dto.Links.EventLink
                {
                    EventId = item.Id,
                    EventName = item.Name
                };

                summary.UserLink = new Dto.Links.AccountLink
                {
                    UserId = creator.Id,
                    UserName = creator.ProfileName
                };

                summary.Date = item.Date;
                summary.DurationInHours = item.Duration;
                summary.Price = (int)item.Price;
                summary.Role = languageRoles.FirstOrDefault(x => x.RoleGroupId == item.RoleGroupId)?.Name;

                summary.Location = $"{item.Location.City}, {eventCountry}"; 
                eventSummaries.Add(summary);
            }

            return eventSummaries;
        }

        public async Task<List<ProfileReview>> GetEventReviews(Guid accountId, string languageCode)
        {
            var identityUser = await this.userManager.FindByIdAsync(accountId.ToString());

            if (identityUser == null)
            {
                return null;
            }

            var customer = this.context.Customers.FirstOrDefault(x => x.IdentityId == accountId.ToString());

            var customerEvents = new List<Event>();

            if (customer.CustomerType == CustomerType.Musician)
            {//get events by musician Id
                customerEvents = this.context.Events
                    .Include(x => x.Reviews)
                    .ThenInclude(y => y.EventReviewPhoto)
                    .Include(x => x.Reviews)
                    .ThenInclude(y => y.Reviewer)
                    .Where(x => x.MusicianId == customer.Id)
                    .ToList();
            }

            if (customer.CustomerType == CustomerType.EventManager)
            {//get events by event manager
                customerEvents = this.context.Events
                    .Include(x => x.Reviews)
                    .ThenInclude(y => y.EventReviewPhoto)
                    .Include(x => x.Reviews)
                    .ThenInclude(y => y.Reviewer)
                    .Where(x => x.EventManagerId == customer.Id)
                    .ToList();
            }

            var eventReviews = new List<ProfileReview>();

            foreach (var item in customerEvents)
            {
                var reviews = item.Reviews;

                var creator = this.context.Customers.FirstOrDefault(x => x.Id == customer.Id);

                if (creator == null)
                {
                    continue;
                }

                foreach (var review in reviews)
                {
                    var profileReview = new ProfileReview();

                    if (review.EventReviewPhoto != null)
                    {
                        var photoReviewPath = $"C:/TurnItUp/EventReviewPhotos/{item.Id}/{review.EventReviewPhoto.Id}/{review.EventReviewPhoto.Name}.{review.EventReviewPhoto.Extension}";
                        var reviewPhotoFile = new List<byte>();

                        if (File.Exists(photoReviewPath))
                        {
                            reviewPhotoFile = File.ReadAllBytes(photoReviewPath).ToList();
                            var photoBytes = reviewPhotoFile.ToArray();
                            var photoBase64 = Convert.ToBase64String(photoBytes);

                            profileReview.EventReviewPhoto = new Dto.Photo
                            {
                                Name = review.EventReviewPhoto.Name,
                                Extension = review.EventReviewPhoto.Extension,
                                Content = photoBase64
                            };
                        }
                    }

                    profileReview.Date = review.ReviewDate;
                    profileReview.Text = review.Text;
                    profileReview.Rating = review.Rating;
                    profileReview.EventLink = new Dto.Links.EventLink
                    {
                        EventId = item.Id,
                        EventName = item.Name
                    };
                    profileReview.UserLink = new Dto.Links.AccountLink
                    {
                        UserId = creator.Id,
                        UserName = creator.ProfileName
                    };

                    eventReviews.Add(profileReview);
                }
            }

            return eventReviews;
        }
    }
}