using Domain.Model.Images;
using Domain.Model.ValueObjects;
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

        public decimal Price { get; set; }

        public string ProfileName { get; set; }

        public CustomerType CustomerType { get; set; }

        public string Description { get; set; }

        public Image ProfilePhoto { get; set; }

        public Image HeaderPhoto {get;set;}

        public List<Gender> Genders { get; set; }

        public List<Role> Roles { get; set; }

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();

		public List<RefreshToken> RefreshTokens => _refreshTokens;

		public void AddRefreshToken(RefreshToken token)
		{
			_refreshTokens.Add(token);
		}

		public bool HasValidRefreshToken(string refreshToken)
		{
			return _refreshTokens.Any(rt => rt.Token == refreshToken && rt.Active);
		}

		public void RemoveRefreshToken(string refreshToken)
		{
			_refreshTokens.Remove(_refreshTokens.First(t => t.Token == refreshToken));
		}
	}
}
