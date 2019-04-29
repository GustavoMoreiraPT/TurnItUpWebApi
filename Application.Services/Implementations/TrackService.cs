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

namespace Application.Services.Implementations
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext context;

        public TrackService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task UploadTrack(int customerId, Track track)
        {
            var customer = this.context.Customers
                .Include(x => x.Tracks)
                .FirstOrDefault(x => x.Id == customerId);

            if (customer == null)
            {
                throw new ArgumentException();
            }

            if (customer.Tracks == null)
            {
                customer.Tracks = new List<Domain.Model.Tracks.Track>();
            }

            Directory.CreateDirectory($@"C:\TurnItUp\Tracks\{customer.Id}");

            byte[] trackBytes = System.Convert.FromBase64String(track.Content);

            float mb = (trackBytes.Length / 1024f) / 1024f;

            if (mb > 50)
            {
                throw new ArgumentNullException();
            }

            File.WriteAllBytes($@"C:\TurnItUp\Tracks\{customer.Id}\{track.Name}.{track.Extension}", trackBytes);

            customer.Tracks.Add(new Domain.Model.Tracks.Track
            {
                Name = track.Name,
                Extension = track.Extension
            });

            this.context.Customers.Update(customer);

            await this.context.SaveChangesAsync();
        }
    }
}
