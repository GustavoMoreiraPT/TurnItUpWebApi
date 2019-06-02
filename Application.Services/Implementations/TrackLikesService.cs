using Application.Services.Interfaces;
using Data.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Application.Dto.Tracks.Responses;

namespace Application.Services.Implementations
{
    public class TrackLikesService : ITrackLikesService
    {
        private readonly ApplicationDbContext context;

        public TrackLikesService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<TrackLikeResponse> LikeTrack(Guid accountId, int trackId)
        {
            var customer = this.context.Customers
                .Include(x => x.Tracks).ThenInclude(y => y.Likes)
                .FirstOrDefault(x => x.IdentityId == accountId.ToString());

            var trackToLike = customer.Tracks.FirstOrDefault(x => x.Id == trackId);

            if (trackToLike == null)
            {
                return new TrackLikeResponse
                {
                    TrackNotFound = true
                };
            }

            if (trackToLike.Likes.Select(x => x.AccountId).ToList().Contains(customer.Id))
            {
                return new TrackLikeResponse
                {
                    TrackAlreadyLiked = true
                };
            }

            trackToLike.Likes.Add(new Domain.Model.Tracks.TrackLike
            {
                AccountId = customer.Id,
                TrackId = trackId,
                Date = DateTime.UtcNow
            });

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();

            return new TrackLikeResponse
            {
                TrackAlreadyLiked = false,
                TrackNotFound = false
            };
        }
    }
}
