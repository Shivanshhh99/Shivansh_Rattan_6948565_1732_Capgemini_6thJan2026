namespace ECommerceLoggingApi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;
    }
}