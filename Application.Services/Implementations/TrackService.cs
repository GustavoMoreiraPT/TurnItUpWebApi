﻿using Application.Dto.Tracks;
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

            Directory.CreateDirectory($@"C:\TurnItUp\Tracks\{customer.Id}");

            var path = Path.Combine(
                       $@"C:\TurnItUp\Tracks\{customer.Id}\",
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
                DurationInSeconds = duration
            };

            customer.Tracks.Add(trackToCreate);

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return new CreateTracksResponse
            {
                TrackId = trackToCreate.Id
            };
        }

        public async Task<List<TrackInfo>> GetTracksInfo(Guid customerId)
        {
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

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
