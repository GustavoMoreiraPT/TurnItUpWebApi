using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Rating
	{
		public int Id { get; set; }

		public decimal Value { get; set; }

		public void SetValue(decimal value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Rating value cannot be less than 0");
			}

			if (value > 5)
			{
				throw new ArgumentException("Rating value cannot be greater than 5");
			}

			this.Value = value;
		}

		public decimal GetValue()
		{
			return this.Value;
		}
	}
}
