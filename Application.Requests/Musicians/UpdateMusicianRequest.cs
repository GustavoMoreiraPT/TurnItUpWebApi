using Application.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Requests.Musicians
{
    public class UpdateMusicianRequest
    {
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public MusicalGenders MusicalGender { get; set; }

        public string Description { get; set; }

        public byte[] ProfilePhoto { get; set; }
    }
}
