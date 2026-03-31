using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required, Range(0.01, 999999.99)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required, Range(0, 10000)]
        [Display(Name = "Stock Quantity")]
        public int StockQuantity { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}