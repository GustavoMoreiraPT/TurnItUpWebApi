using Domain.Model.Events;
using Domain.Model.Images;
using Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.Reviews
{
    public class EventReview
    {
        public int Id { get; set; }

        public int ReviewerId { get; set; }

        public int EventId { get; set; }

        public Image EventReviewPhoto { get; set; }

        public DateTime ReviewDate { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        public virtual Customer Reviewer { get; set; }

        public virtual Event Event{ get;set; }
    }
}
