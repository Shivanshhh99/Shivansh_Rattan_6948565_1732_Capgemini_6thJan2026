using System;
using System.Collections.Generic;

namespace E_Commerce_Application.Models
{
	public class Order
	{
		public int OrderId { get; set; }

		public int CustomerId { get; set; }

		public Customer? Customer { get; set; }

		public DateTime OrderDate { get; set; }

		public decimal TotalAmount { get; set; }

		public ICollection<OrderDetail>? OrderDetails { get; set; }
	}
}