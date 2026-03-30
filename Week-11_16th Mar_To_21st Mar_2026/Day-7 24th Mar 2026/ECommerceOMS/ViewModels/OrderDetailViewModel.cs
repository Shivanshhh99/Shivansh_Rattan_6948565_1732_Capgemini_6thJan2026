namespace ECommerceOMS.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public List<OrderItemViewModel> Items { get; set; } = new();

        // Shipping
        public string ShippingAddress { get; set; } = string.Empty;
        public string CarrierName { get; set; } = string.Empty;
        public string TrackingNumber { get; set; } = string.Empty;
        public string ShippingStatus { get; set; } = string.Empty;
        public DateTime? EstimatedDelivery { get; set; }
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}