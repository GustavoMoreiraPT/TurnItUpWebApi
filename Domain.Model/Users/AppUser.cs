﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Model.Users
{
	public class AppUser : IdentityUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public long? FacebookId { get; set; }

		public string PictureUrl { get; set; }
	}
}
