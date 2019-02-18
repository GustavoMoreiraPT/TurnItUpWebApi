using System;

namespace Application.Dto.Reviews
{
    public class Review
    {
        public string Text { get; set; }

        public DateTime Date { get; set; }

        public string DigitalContentName { get; set; }

        public string ReviewerName { get; set; }

        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
