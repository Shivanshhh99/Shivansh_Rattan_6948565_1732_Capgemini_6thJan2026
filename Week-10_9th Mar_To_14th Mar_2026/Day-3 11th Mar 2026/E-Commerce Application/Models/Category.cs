using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_Application.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[Required]
		public string Name { get; set; }

		public string? Description { get; set; }

		public ICollection<Product>? Products { get; set; }
	}
}