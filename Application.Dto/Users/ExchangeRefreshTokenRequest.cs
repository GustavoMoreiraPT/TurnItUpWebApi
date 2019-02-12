using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.CrossCutting.Helpers;

namespace Application.Dto.Users
{
	public class ExchangeRefreshTokenRequest : IUseCaseRequest<ExchangeRefreshTokenResponse>
	{
		public string AccessToken { get; }

		public string RefreshToken { get; }

		public string SigningKey { get; }

		public ExchangeRefreshTokenRequest(string accessToken, string refreshToken, string signingKey)
		{
			this.AccessToken = accessToken;
			this.RefreshToken = refreshToken;
			this.SigningKey = signingKey;
		}
	}
}
