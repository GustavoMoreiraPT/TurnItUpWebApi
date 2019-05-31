using Application.Services.Interfaces;
using Data.Repository.Configuration;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementations
{
    public class TrackPlayService : ITrackPlayService
    {
        private readonly ApplicationDbContext context;

        public TrackPlayService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task PlayTrack(Guid accountId, int trackId)
        {
            var customer = this.context.Customers
                .Include(x => x.Tracks).ThenInclude(y => y.Plays)
                .FirstOrDefault(x => x.IdentityId == accountId.ToString());

            var trackToPlay = customer.Tracks.FirstOrDefault(x => x.Id == trackId);

            if (trackToPlay == null)
            {
                return;
            }

            trackToPlay.Plays.Add(new Domain.Model.Tracks.TrackPlay
            {
                AccountId = customer.Id,
                TrackId = trackId,
                Date = DateTime.UtcNow
            });

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();
        }
    }
}
