using System.ComponentModel.DataAnnotations;

namespace ECommerceLoggingApi.DTOs
{
    public class OrderRequestDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}