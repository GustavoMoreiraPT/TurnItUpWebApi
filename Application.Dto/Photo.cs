using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class Photo
    {
        public string Name { get; set; }

        public byte [] Content { get; set; }

        public string Extension { get; set; }
    }
}
