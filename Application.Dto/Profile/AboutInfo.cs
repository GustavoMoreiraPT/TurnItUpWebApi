using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Profile
{
    public class AboutInfo
    {
        public decimal Price { get; set; }

        public List<string> Genres { get; set; }

        public string Country { get; set; }

        public DescriptionText Description { get; set; }
    }
}
