using Application.Requests.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Users
{
    public class RegisterEditDto
    {
        public AccountType AccountType { get; set; }

        public string Email { get; set; }

        public string ProfileName { get; set; }

        public Photo ProfilePhoto { get; set; }
        
        public Photo HeaderPhoto { get; set; }

        public List<RoleDto> Roles { get; set; }

        public List<GenreDto> Genres { get; set; }

        public decimal Price { get; set; }

        public LocationDto Location { get; set; }

        public string Description { get; set; }
    }
}
