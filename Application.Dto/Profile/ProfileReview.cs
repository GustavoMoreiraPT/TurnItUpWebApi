using Application.Dto.Links;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Profile
{
    public class ProfileReview
    {
        public DateTime Date { get; set; }

        public Photo EventReviewPhoto { get; set; }

        public string Text { get; set; }

        public decimal Rating { get; set; }

        public EventLink EventLink { get; set; }

        public UserLink UserLink { get; set; }
    }
}
