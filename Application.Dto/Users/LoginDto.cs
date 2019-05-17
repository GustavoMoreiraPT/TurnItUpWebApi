using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Users
{
	[Validator(typeof(LoginDto))]
	public class LoginDto
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
