using Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.SocialMedia
{
    public class SocialNetwork
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Customer Customer { get; set; }
    }
}
