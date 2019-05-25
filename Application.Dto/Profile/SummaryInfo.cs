using Application.Dto.Enum;
using Application.Dto.SocialMedia;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Profile
{
    public class SummaryInfo
    {
        public Photo ProfilePhoto { get; set; }

        public Photo CoverPhoto { get; set; }

        public string Name { get; set; }

        public List<string> Roles { get; set; }

        public int FollowersCount { get; set; }

        public List<SocialNetwork> SocialMediaLinks { get; set; }

        public decimal Rating { get; set; }

        public int ReviewsCount { get; set; }

        public decimal Price { get; set; }

        public List<string> Genres { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public AccountTypes Type { get; set; }
    }
}
