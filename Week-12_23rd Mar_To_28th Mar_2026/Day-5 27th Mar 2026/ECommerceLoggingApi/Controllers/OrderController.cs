using ECommerceLoggingApi.DTOs;
using ECommerceLoggingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(ILogger<OrderController> logger, OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderRequestDto request)
        {
            try
            {
                _logger.LogInformation("Order creation started for UserId: {UserId}, ProductId: {ProductId}",
                    request.UserId, request.ProductId);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid order input received for UserId: {UserId}", request.UserId);
                    return BadRequest(ModelState);
                }

                var order = _orderService.CreateOrder(request);

                _logger.LogInformation("Order created successfully. OrderId: {OrderId}, UserId: {UserId}",
                    order.Id, order.UserId);

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Order creation failed for UserId: {UserId}, ProductId: {ProductId}",
                    request.UserId, request.ProductId);

                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}