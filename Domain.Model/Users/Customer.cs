using System.Collections.Generic;
using System.Linq;

namespace Domain.Model.Users
{
	public class Customer
	{
		public int Id { get; set; }

		public string IdentityId { get; set; }

		public AppUser Identity { get; set; }  // navigation property

		public string Location { get; set; }

		public string Locale { get; set; }

		public string Gender { get; set; }

		private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();

		public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

		public void AddRefreshToken(RefreshToken token)
		{
			_refreshTokens.Add(token);
		}

		public bool HasValidRefreshToken(string refreshToken)
		{
			return _refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
		}
	}
}
