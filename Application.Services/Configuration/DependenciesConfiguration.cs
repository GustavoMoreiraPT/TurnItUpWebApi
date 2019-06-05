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

			services.AddSingleton<IMusicianService, MusicianService>();
            services.AddSingleton<IRecruiterService, RecruiterService>();
            services.AddSingleton<ICountriesService, CountriesService>();
            services.AddSingleton<IRolesService, RolesService>();
            services.AddSingleton<IGenresService, GenresService>();
            services.AddSingleton<ITrackService, TrackService>();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IFollowersService, FollowersService>();
            services.AddSingleton<ITrackLikesService, TrackLikesService>();
            services.AddSingleton<ITrackPlayService, TrackPlayService>();

            services.AddSingleton<UserManager<AppUser>>();

			services.AddSingleton<IUsersService, UsersService>();
			services.AddSingleton<IJwtFactory, JwtFactory>();
			services.AddSingleton<ITokenFactory, TokenFactory>();
			services.AddSingleton<IJwtTokenValidator, JwtTokenValidator>();
			services.AddSingleton<IJwtTokenHandler, JwtTokenHandler>();

			//TEMPORARY!! JUST TO MAKE IT WORK FOR NOW
			services.AddSingleton<IRepository<Customer>, EfRepository<Customer>>();

			return services;
		}
	}
}
