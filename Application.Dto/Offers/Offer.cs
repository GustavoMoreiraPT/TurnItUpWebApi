using Application.Requests.Enums;
using System;

namespace Application.Dto.Offers
{
    public class Offer
    {
        public MusicalGenders MusicalGender { get; set; }

        public DateTime PerformanceDate { get; set; }

        public Local PerformanceLocal { get; set; }

        public float ExpectedPrice { get; set; }

        public string Description { get; set; }

        public string DestinationUserName { get; set; }

        public OfferStatus Status { get; set; }
    }
}
