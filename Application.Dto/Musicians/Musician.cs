using Application.Dto.Offers;
using Application.Dto.Performances;
using Application.Dto.Reviews;
using Application.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Musicians
{
    public class Musician
    {
        public Uri ProfilePhoto { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime BornDate { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public MusicalGenders MusicalGender { get; set; }

        public List<Performance> pastPerformances { get; set; }

        public List<Performance> scheduledPerformances { get; set; }

        public List<Offer> Offers { get; set; }

        public List<Review> Reviews { get; set; }

        public int Followers { get; set; }
    }
}
