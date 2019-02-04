using System;
using System.Collections.Generic;
using System.Text;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
	public class UsersService : IUsersService
	{
		public UsersService()
		{

		}

		public void RevokeRefreshToken(string token)
		{
			throw new NotImplementedException();
		}

		public void SignUp(string username, string password)
		{
			throw new NotImplementedException();
		}
	}
}
