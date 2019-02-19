using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Age
	{
		public int Id { get; set; }

		public int Value { get; set; }

		public void SetAge(int age)
		{
			if (age < 6)
			{
				throw new ArgumentException("The user cannot have less than 6 years old.");
			}

			this.Value = age;
		}

		public int GetAge()
		{
			return this.Value;
		}
	}
}
