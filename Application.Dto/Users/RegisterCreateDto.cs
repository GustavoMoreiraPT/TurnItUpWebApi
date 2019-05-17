using System.ComponentModel.DataAnnotations;
using Application.Dto.Enum;
using Application.Requests.Enums;

namespace Application.Dto.Users
{
	public class RegisterCreateDto
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
