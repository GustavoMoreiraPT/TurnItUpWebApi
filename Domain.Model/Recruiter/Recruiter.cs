using Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Domain.Model.Recruiter
{
	public class Recruiter : TurnItUpUser
	{
		public string Name { get; set; }

		public string Email { get; set; }
    }
}
