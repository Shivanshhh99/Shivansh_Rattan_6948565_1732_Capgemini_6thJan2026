using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SecureJwtLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;

            _logger.LogInformation("GET /api/orders called by UserId: {UserId}, Username: {Username}", userId, username);

            return Ok(new[]
            {
                new { OrderId = 101, Product = "Laptop", Amount = 65000 },
                new { OrderId = 102, Product = "Mobile", Amount = 25000 }
            });
        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;

            _logger.LogInformation("POST /api/orders called by UserId: {UserId}, Username: {Username}", userId, username);

            return Ok(new { Message = "Order created successfully" });
        }
    }
}