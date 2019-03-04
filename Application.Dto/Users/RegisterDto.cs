using System.ComponentModel.DataAnnotations;
using Application.Dto.Enum;
using Application.Requests.Enums;

namespace Application.Dto.Users
{
	public class RegisterDto
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Location { get; set; }

		public AccountTypes AccountType { get; set; }
	}
}
