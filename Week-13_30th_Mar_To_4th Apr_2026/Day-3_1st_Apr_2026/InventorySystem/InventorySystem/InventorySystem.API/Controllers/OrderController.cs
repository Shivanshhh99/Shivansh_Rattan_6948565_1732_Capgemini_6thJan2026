using Microsoft.AspNetCore.Mvc;
using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Place a new order.
        /// Participants should implement this method to:
        /// - Call _orderService.PlaceOrderAsync(order)
        /// - If successful (returns true), return 201 Created with the order details
        /// - If failed (returns false), return 400 Bad Request
        /// </summary>
        /// <param name="order">The order to place</param>
        /// <returns>201 Created or 400 Bad Request</returns>
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order cannot be null");
            }

            var success = await _orderService.PlaceOrderAsync(order);

            if (!success)
            {
                return BadRequest("Failed to place order");
            }

            return CreatedAtAction(nameof(PlaceOrder), new { id = order.Id }, order);
        }
    }
}
