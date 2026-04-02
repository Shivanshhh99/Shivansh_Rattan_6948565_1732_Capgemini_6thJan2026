using System.ComponentModel.DataAnnotations;

namespace ECommerceLoggingApi.DTOs
{
    public class PaymentRequestDto
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }
    }
}