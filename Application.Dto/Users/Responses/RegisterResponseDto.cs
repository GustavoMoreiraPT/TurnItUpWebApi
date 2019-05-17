using Infrastructure.CrossCutting.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Application.Dto.Users.Responses
{
    public class RegisterResponseDto
    {
        public string UserCreatedId { get; set; }

        public IdentityResult IdentityResult { get; set; }

        public List<Error> Errors { get; set; }
    }
}
