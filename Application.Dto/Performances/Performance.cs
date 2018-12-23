using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Performances
{
    public class Performance
    {
        public string OriginUserName { get; set; }

        public string DestinationUserName { get; set; }

        public DateTime PerformanceDate { get; set; }

        public Local PerformanceLocal { get; set; }

        public float FinalPrice { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }
    }
}
