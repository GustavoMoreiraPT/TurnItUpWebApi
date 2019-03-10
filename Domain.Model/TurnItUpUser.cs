using Domain.Model.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model
{
	public class TurnItUpUser
	{
		public int Id { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
	}
}
