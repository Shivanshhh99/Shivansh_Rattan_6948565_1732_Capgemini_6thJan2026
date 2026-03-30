using System.ComponentModel.DataAnnotations;

namespace ECommerceOMS.Models
{
    public enum ShippingStatus
    {
        NotShipped,
        Preparing,
        Dispatched,
        InTransit,
        Delivered
    }

    public class ShippingDetail
    {
        public int Id { get; set; }

        // One-to-One with Order (shared primary key pattern)
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Required, StringLength(250)]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Carrier Name")]
        public string CarrierName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Estimated Delivery")]
        public DateTime? EstimatedDelivery { get; set; }

        [Required]
        public ShippingStatus Status { get; set; } = ShippingStatus.NotShipped;
    }
}