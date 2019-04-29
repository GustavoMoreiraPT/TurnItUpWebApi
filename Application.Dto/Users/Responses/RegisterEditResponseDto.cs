using Infrastructure.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto.Users.Responses
{
    public class RegisterEditResponseDto
    {
        public List<Error> Errors { get; set; }
    }
}
