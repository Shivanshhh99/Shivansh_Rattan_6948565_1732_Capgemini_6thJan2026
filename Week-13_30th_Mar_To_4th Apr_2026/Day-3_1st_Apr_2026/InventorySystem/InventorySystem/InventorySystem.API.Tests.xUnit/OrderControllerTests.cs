using Xunit;
using Moq;
using InventorySystem.API.Controllers;
using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Tests.xUnit
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderService> _mockOrderService;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            // TODO: Initialize mock and controller in constructor
            // This demonstrates xUnit's constructor injection pattern
            _mockOrderService = new Mock<IOrderService>();
            _controller = new OrderController(_mockOrderService.Object);
        }

        [Fact]
        public async Task PlaceOrder_WithValidOrder_ReturnsCreatedResult()
        {
            // Arrange: Create a valid order and setup the mock to return true
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                Items = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 5, Price = 99.99m } },
                OrderDate = DateTime.Now
            };
            _mockOrderService.Setup(s => s.PlaceOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(true);

            // Act: Call PlaceOrder with the valid order
            var result = await _controller.PlaceOrder(order);

            // Assert: Verify the result is CreatedResult (201 Created)
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PlaceOrder_WithInvalidOrder_ReturnsBadRequestResult()
        {
            // Arrange: Create an invalid order and setup the mock to return false
            var order = new Order
            {
                Id = 2,
                CustomerId = 2,
                Items = new List<OrderItem> { new OrderItem { ProductId = 1, Quantity = 0, Price = 99.99m } }
            };
            _mockOrderService.Setup(s => s.PlaceOrderAsync(It.IsAny<Order>()))
                .ReturnsAsync(false);

            // Act: Call PlaceOrder with the invalid order
            var result = await _controller.PlaceOrder(order);

            // Assert: Verify the result is BadRequestResult (400 Bad Request)
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
