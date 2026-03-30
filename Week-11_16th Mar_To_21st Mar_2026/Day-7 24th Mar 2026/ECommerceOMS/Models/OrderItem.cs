using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, Range(1, 1000)]
        public int Quantity { get; set; }

        [Required, Range(0.01, 999999.99)]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal => Quantity * UnitPrice;
    }
}