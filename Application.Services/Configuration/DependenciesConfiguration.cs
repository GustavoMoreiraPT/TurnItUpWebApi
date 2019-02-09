﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repository.Configuration;
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

			return services;
		}
	}
}
