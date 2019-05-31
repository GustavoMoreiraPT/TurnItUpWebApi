using Application.Dto.Profile;
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
                customerEvents = this.context.Events.Where(x => x.MusicianId == customer.Id).ToList();
            }

            if (customer.CustomerType == CustomerType.EventManager)
            {//get events by event manager
                customerEvents = this.context.Events.Where(x => x.EventManagerId == customer.Id).ToList();
            }

            var languageRoles = this.context.LanguageRoles.Where(x => x.Language == languageCode);

            var eventSummaries = new List<EventSummary>();

            foreach (var item in customerEvents)
            {
                var summary = new EventSummary();

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

                eventSummaries.Add(summary);
            }

            return eventSummaries;
        }
    }
}
