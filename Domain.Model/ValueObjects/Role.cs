using Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Role
	{
		public int Id { get; set; }

		public int GroupId { get; set; }

        public Customer customer { get; set; }
	}
}
