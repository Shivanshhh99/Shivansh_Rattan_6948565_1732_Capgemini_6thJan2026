namespace ECommerceOMS.ViewModels
{
    public class DashboardViewModel
    {
        public List<TopProductViewModel> TopProducts { get; set; } = new();
        public List<PendingOrderViewModel> PendingOrders { get; set; } = new();
        public List<CustomerSummaryViewModel> CustomerSummaries { get; set; } = new();
        public int TotalOrders { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalProducts { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class TopProductViewModel
    {
        public string ProductName { get; set; } = string.Empty;
        public int TotalSold { get; set; }
        public decimal Revenue { get; set; }
    }

    public class PendingOrderViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class CustomerSummaryViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
    }
}