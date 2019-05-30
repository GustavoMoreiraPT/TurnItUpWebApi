using Application.Dto.SocialMedia;
using Application.Requests.Enums;
using System.Collections.Generic;

namespace Application.Dto.Users
{
    public class RegisterEditDto
    {
        public AccountType AccountType { get; set; }

        public string ProfileName { get; set; }

        public Photo ProfilePhoto { get; set; }
        
        public Photo HeaderPhoto { get; set; }

        public List<int> RoleGroup { get; set; }

        public List<int> GenresGroup { get; set; }

        public List<SocialNetwork> SocialNetworks { get; set; }

        public decimal Price { get; set; }

        public LocationDto Location { get; set; }

        public string Description { get; set; }
    }
}
