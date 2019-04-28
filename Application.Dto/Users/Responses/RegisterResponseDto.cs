using Infrastructure.CrossCutting.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Users.Responses
{
    public class RegisterResponseDto
    {
        public string UserCreatedId { get; set; }

        public IdentityResult IdentityResult { get; set; }

        public List<Error> Errors { get; set; }
    }
}
