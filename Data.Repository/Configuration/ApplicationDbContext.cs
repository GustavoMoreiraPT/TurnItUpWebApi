using System;
using System.Collections.Generic;
using System.Text;
using Domain.Model;
using Domain.Model.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Configuration
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Customer> Customers { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

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
