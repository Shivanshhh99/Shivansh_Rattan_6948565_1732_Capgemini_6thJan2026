using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Application.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public string Email { get; set; }

		public string? Address { get; set; }

		public ICollection<Order>? Orders { get; set; }
	}
}