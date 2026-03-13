using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Application.Models
{
	public class Product
	{
		public int ProductId { get; set; }

		[Required]
		public string Name { get; set; }

		public decimal Price { get; set; }

		public int StockQuantity { get; set; }

		public int CategoryId { get; set; }

		public Category? Category { get; set; }

		public ICollection<OrderDetail>? OrderDetails { get; set; }
	}
}