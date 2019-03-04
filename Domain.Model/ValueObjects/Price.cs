using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Model.ValueObjects
{
	public class Price
	{
		public int Id { get; set; }

		public decimal Value { get; set; }

		public Price(decimal value)
		{
			this.Value = value;
		}

		public void SetPrice(decimal value)
		{
			if (value < 0)
			{
				throw new ArgumentException("Price value cannot be less than 0");
			}

			this.Value = value;
		}

		public decimal GetPrice()
		{
			return this.Value;
		}
	}
}
