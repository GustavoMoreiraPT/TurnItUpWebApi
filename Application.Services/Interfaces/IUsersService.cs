using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Interfaces
{
	public interface IUsersService
	{
		void SignUp(string username, string password);
		void RevokeRefreshToken(string token);
	}
}
