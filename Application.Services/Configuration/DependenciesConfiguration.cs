using System;
using System.Collections.Generic;
using System.Text;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Configuration
{
	public static class DependenciesConfiguration
	{
		public static IServiceCollection ConfigureDependencies(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddSingleton<IUsersService, UsersService>();

			return services;
		}
	}
}
