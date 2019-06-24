using Application.Dto.Tracks;
using Application.Services.Interfaces;
using Data.Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.IO;
using Application.Dto.Tracks.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Domain.Model.Users;
using Application.Dto;

namespace Application.Services.Implementations
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<AppUser> userManager;

        public TrackService(ApplicationDbContext context, UserManager<AppUser> userRepository)
        {
            this.context = context;
            this.userManager = userRepository;
        }

        public async Task<CreateTracksResponse> UploadTrack(Guid customerId, IFormFile track)
        {
            var baseTrackPath = $@"TurnItUp\Tracks";

            var identityUser = await this.userManager.FindByIdAsync(customerId.ToString());

            if (identityUser == null)
            {
                return new CreateTracksResponse
                {
                    Errors = new List<Infrastructure.CrossCutting.Helpers.Error>
                    {
                        new Infrastructure.CrossCutting.Helpers.Error("Invalid track request", "Customer id provided does not belong to any customer")
                    }
                };
            }

            var customer = this.context.Customers
                .Include(x => x.Tracks)
                .FirstOrDefault(x => x.IdentityId == identityUser.Id);

            if (customer == null)
            {
                return new CreateTracksResponse
                {
                    Errors = new List<Infrastructure.CrossCutting.Helpers.Error>
                    {
                        new Infrastructure.CrossCutting.Helpers.Error("Invalid track request", "Customer id provided does not belong to any customer")
                    }
                };
            }

            if (customer.Tracks == null)
            {
                customer.Tracks = new List<Domain.Model.Tracks.Track>();
            }

            Directory.CreateDirectory($@"C:\{baseTrackPath}\{customer.IdentityId}");

            var path = Path.Combine(
                       $@"C:\{baseTrackPath}\{customer.IdentityId}\",
                       track.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await track.CopyToAsync(stream);
            }

            TagLib.File fileInfo = TagLib.File.Create(path, TagLib.ReadStyle.Average);
            var duration = (int)fileInfo.Properties.Duration.TotalSeconds;

            float mb = track.Length / 1024 / 1024;

            if (mb > 50)
            {
                return new CreateTracksResponse
                {
                    Errors = new List<Infrastructure.CrossCutting.Helpers.Error>
                    {
                        new Infrastructure.CrossCutting.Helpers.Error("Invalid track request", "File to upload cannot exceed 50 mb")
                    }
                };
            }

            var trackToCreate = new Domain.Model.Tracks.Track
            {
                Name = track.FileName.Split('.')[0],
                Extension = track.FileName.Split('.')[1],
                DurationInSeconds = duration,
                TrackAudioLocation = $@"{baseTrackPath}\{customer.IdentityId}\{track.FileName}"
            };

            customer.Tracks.Add(trackToCreate);

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return new CreateTracksResponse
            {
                TrackId = trackToCreate.Id,
                Errors = new List<Infrastructure.CrossCutting.Helpers.Error>()
            };
        }

        public async Task<List<TrackInfo>> GetTracksInfo(Guid customerId)
        {
            var trackPhotosBasePath = $@"TurnItUp\TrackPhotos";
            var trackAudioBasePath = $@"TurnItUp\Tracks";

            var customer = this.context.Customers
                .Include(x => x.Tracks)
                .ThenInclude(y => y.Likes)
                .Include(x => x.Tracks)
                .ThenInclude(y => y.Plays)
                .FirstOrDefault(x => x.IdentityId == customerId.ToString());

            if (customer == null)
            {
                return null;
            }

            return customer.Tracks.Select(x => new TrackInfo
            {
                TrackId = x.Id,
                Title = x.Name,
                Photo = null,
                TrackDurationTime = x.DurationInSeconds,
                TrackAudioLocation = x.TrackAudioLocation,
                TrackPhotoLocation = x.TrackPhotoLocation,
                LikesCount = x.Likes.Count,
                PlaysCount = x.Plays.Count
            }).ToList();
        }

        public async Task<bool> DeleteTrack(Guid customerId, int trackId)
        {
            var customer = this.context.Customers
                .Include(x => x.Tracks)
                .ThenInclude(y => y.Plays)
                .Include(x => x.Tracks)
                .ThenInclude(y => y.Likes)
                .FirstOrDefault(x => x.IdentityId == customerId.ToString());

            var trackToRemove = customer.Tracks.FirstOrDefault(x => x.Id == trackId);

            if (trackToRemove == null)
            {
                return false;
            }

            customer.Tracks.Remove(trackToRemove);

            File.Delete($@"C:\TurnItUp\Tracks\{customer.IdentityId}\{trackToRemove.Name}.{trackToRemove.Extension}");

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<CreatePhotoTrackResponse> UploadTrackPhoto(Guid customerId, int trackId, Photo photo)
        {
            var trackPhotosBasePath = $@"TurnItUp\TrackPhotos";

            var customer = this.context.Customers
                .Include(x => x.Tracks)
                .ThenInclude(x => x.TrackPhoto)
                .FirstOrDefault(x => x.IdentityId == customerId.ToString());

            var trackToUpdatePhoto = customer.Tracks.FirstOrDefault(x => x.Id == trackId);

            if (trackToUpdatePhoto == null)
            {
                return new CreatePhotoTrackResponse
                {
                    Errors = new List<Infrastructure.CrossCutting.Helpers.Error>
                    {
                        new Infrastructure.CrossCutting.Helpers.Error("404", string.Empty)
                    }
                };
            }

            byte[] trackPhotoBytes = System.Convert.FromBase64String(photo.Content);

            Directory.CreateDirectory($@"C:\{trackPhotosBasePath}\{customer.IdentityId}");

            File.WriteAllBytes($@"C:\{trackPhotosBasePath}\{customer.IdentityId}\{trackId}\{photo.Name}.{photo.Extension}", trackPhotoBytes);

            trackToUpdatePhoto.TrackPhoto = new Domain.Model.Images.Image
            {
                Name = photo.Name,
                Extension = photo.Extension
            };

            trackToUpdatePhoto.TrackPhotoLocation = $@"{trackPhotosBasePath}\{customer.IdentityId}\{trackId}\{photo.Name}.{photo.Extension}";

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return new CreatePhotoTrackResponse
            {
                PhotoId = trackToUpdatePhoto.TrackPhoto.Id,
                Errors = new List<Infrastructure.CrossCutting.Helpers.Error>()
            };
        }
    }
}
