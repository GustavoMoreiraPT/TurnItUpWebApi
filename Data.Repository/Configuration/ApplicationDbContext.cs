using System.Diagnostics;
using Domain.Model;
using Domain.Model.Events;
using Domain.Model.Images;
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

		public DbSet<Country> Countries { get; set; }

		public DbSet<Gender> Genders { get; set; }

		public DbSet<Location> Locations { get; set; }

		public DbSet<Price> Prices { get; set; }

		public DbSet<Rating> Ratings { get; set; }

		public DbSet<Role> Role { get; set; }

		public DbSet<Event> Events { get; set; }

		public DbSet<EventState> EventState { get; set; }

		public DbSet<EventLocation> EventLocations { get; set; }

        public DbSet<Image> Images { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(GetConnectionString(), b => b.MigrationsAssembly("Data.Repository"));
		}

		private static string GetConnectionString()
		{
			const string databaseName = "webapijwt";

			const string databaseUser = "root";

			const string databasePass = "Amsterdam2018";



			return $"Server=localhost;" +

				   $"database={databaseName};" +

				   $"uid={databaseUser};" +

				   $"pwd={databasePass};" +

				   $"pooling=true;";
		}
	}
}
