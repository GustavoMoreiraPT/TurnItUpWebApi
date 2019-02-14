using Application.Services.Handlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Domain.Model.Users;
using Infrastructure.CrossCutting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.Configuration
{
	public static class DependenciesConfiguration
	{
		public static IServiceCollection ConfigureDependencies(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddScoped<UserManager<AppUser>>();

			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IJwtFactory, JwtFactory>();
			services.AddScoped<ITokenFactory, TokenFactory>();
			services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();
			services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

			return services;
		}
	}
}
