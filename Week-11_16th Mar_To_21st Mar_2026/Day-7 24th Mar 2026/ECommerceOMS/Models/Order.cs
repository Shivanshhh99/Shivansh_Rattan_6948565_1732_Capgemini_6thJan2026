using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ShippingDetail? ShippingDetail { get; set; }
    }
}