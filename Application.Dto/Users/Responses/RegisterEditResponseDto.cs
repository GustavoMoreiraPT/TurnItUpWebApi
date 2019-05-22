using Infrastructure.CrossCutting.Helpers;
using System.Collections.Generic;

namespace Application.Dto.Users.Responses
{
    public class RegisterEditResponseDto
    {
        public List<Error> Errors { get; set; }
    }
}
