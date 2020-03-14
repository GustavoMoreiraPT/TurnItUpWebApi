using Domain.Model;
using Domain.Model.Events;
using Domain.Model.Genres;
using Domain.Model.Images;
using Domain.Model.Reviews;
using Domain.Model.Roles;
using Domain.Model.SocialMedia;
using Domain.Model.Tracks;
using Domain.Model.Users;
using Domain.Model.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Configuration
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Age> Ages { get; set; }

        //Collections from the user
        public DbSet<Country> CustomerCountries { get; set; }

        public DbSet<Gender> CustomerGenres { get; set; }

        public DbSet<Role> CustomerRoles { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Price> Prices { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Image> Images { get; set; }

        //Set of collections associated to a specific country Code: Usage: search for countryCode.

        public DbSet<LanguageRole> LanguageRoles { get; set; }

        public DbSet<LanguageGenrer> LanguageGenres { get; set; }

        public DbSet<LanguageCountry> LanguageCountries { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        //Collections groups
        public DbSet<CountryGroup> GroupsOfCountries { get; set; }

        public DbSet<RoleGroup> GroupsOfRoles { get; set; }

        public DbSet<GenrerGroup> GroupsOfGenrers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventReview> EventReviews { get; set; }

        public DbSet<TrackLike> TrackLikes { get; set; }

        public DbSet<TrackPlay> TrackPlays { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(GetConnectionString(), b => b.MigrationsAssembly("Data.Repository"));
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Genders);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Tracks);

            modelBuilder.Entity<Track>().HasMany(c => c.Plays);
            modelBuilder.Entity<Track>().HasMany(c => c.Likes);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Roles)
                .WithOne(x => x.customer);
        }

        private static string GetConnectionString()
		{
			const string databaseName = "heroku_3ce26466163290f";

			const string databaseUser = "b5d2a09c212b13";

			const string databasePass = "4d98dba6";



			return $"Server=eu-cdbr-west-02.cleardb.net;" +

				   $"database={databaseName};" +

				   $"uid={databaseUser};" +

				   $"pwd={databasePass};" +

				   $"pooling=true;";
		}
	}
}
