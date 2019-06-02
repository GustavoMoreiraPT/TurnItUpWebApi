using Domain.Model.Images;
using Domain.Model.ValueObjects;
using Domain.Model.Tracks;
using System.Collections.Generic;
using System.Linq;
using Domain.Model.SocialMedia;

namespace Domain.Model.Users
{
    public class Customer
    {
        public int Id { get; set; }

        public string IdentityId { get; set; }

        public AppUser Identity { get; set; }  // navigation property

        public Location Location { get; set; }

        public string Locale { get; set; }

        public string Gender { get; set; }

        public decimal Price { get; set; }

        public string ProfileName { get; set; }

        public decimal Rating { get; set; }

        public int FollowersCount { get; set; }

        public int ReviewsCount { get; set; }

        public CustomerType CustomerType { get; set; }

        public string Description { get; set; }

        public Image ProfilePhoto { get; set; }

        public Image HeaderPhoto {get;set;}

        public ICollection<Track> Tracks { get; set; }

        public ICollection<Gender> Genders { get; set; }

        public ICollection<Role> Roles { get; set; }

        public ICollection<SocialNetwork> SocialNetworks { get; set; }

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
