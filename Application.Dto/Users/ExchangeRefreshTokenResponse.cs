using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.CrossCutting.Helpers;

namespace Application.Dto.Users
{
	public class ExchangeRefreshTokenResponse : UseCaseResponseMessage
	{
		public AccessToken AccessToken { get; }

		public string RefreshToken { get; }

		public ExchangeRefreshTokenResponse(bool success = false, string message = null) : base(success, message){}

		public ExchangeRefreshTokenResponse(AccessToken accessToken, string refreshToken, bool success = false,
			string message = null) : base(success, message)
		{
			this.AccessToken = accessToken;
			this.RefreshToken = refreshToken;
		}
	}
}
