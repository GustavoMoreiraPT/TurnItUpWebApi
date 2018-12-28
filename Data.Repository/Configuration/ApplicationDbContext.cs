using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Configuration
{
	public class ApplicationDbContext : IdentityDbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(GetConnectionString());
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
