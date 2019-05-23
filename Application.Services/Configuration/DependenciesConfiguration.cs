using Application.Services.Handlers;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using Data.Repository.ImplementedRepositories;
using Domain.Core.RepositoryInterfaces;
using Domain.Model.Users;
using Infrastructure.CrossCutting;
using Infrastructure.CrossCutting.Logging;
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
			//Infra
			services.AddSingleton<ILogger, Logger>();

			services.AddScoped<IMusicianService, MusicianService>();
            services.AddScoped<IRecruiterService, RecruiterService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IFollowersService, FollowersService>();

            services.AddScoped<UserManager<AppUser>>();

			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IJwtFactory, JwtFactory>();
			services.AddScoped<ITokenFactory, TokenFactory>();
			services.AddScoped<IJwtTokenValidator, JwtTokenValidator>();
			services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

			//TEMPORARY!! JUST TO MAKE IT WORK FOR NOW
			services.AddScoped<IRepository<Customer>, EfRepository<Customer>>();

			return services;
		}
	}
}
