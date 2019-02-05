using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Users;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Repository.Configuration;
using Domain.Model.Users;
using Infrastructure.CrossCutting.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Services.Implementations
{
	public class UsersService : IUsersService
	{
		private readonly ApplicationDbContext identityDbContext;
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;

		public UsersService(ApplicationDbContext identityDbContext, UserManager<AppUser> userManager, IMapper mapper)
		{
			this.identityDbContext = identityDbContext;
			this.userManager = userManager;
			this.mapper = mapper;
		}

		public async Task<IdentityResult> CreateUserAsync(RegisterDto user, string password)
		{
			var userIdentity = this.mapper.Map<AppUser>(user);

			var result = await this.userManager.CreateAsync(userIdentity, password).ConfigureAwait(false);

			if (!result.Succeeded)
			{
				return null;
			}

			await this.identityDbContext.Customers.AddAsync(new Customer{ IdentityId = userIdentity.Id, Location = user.Location});
			await this.identityDbContext.SaveChangesAsync();

			return result;
		}
	}
}
