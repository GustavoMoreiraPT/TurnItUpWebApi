using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Interfaces
{
	public interface IUsersService
	{
		Task<IdentityResult> CreateUserAsync(RegisterDto user, string password);
	}
}
