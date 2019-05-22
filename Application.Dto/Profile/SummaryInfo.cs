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

        public string PlaceHolderName { get; set; }

        public Local Location { get; set; }

        public int FollowersCount { get; set; }

        public List<SocialMediaLink> SocialMediaLinks { get; set; }

        public decimal Rating { get; set; }

        public int ReviewsCount { get; set; }
    }
}
