using Xunit;
using Moq;
using InventorySystem.API.Controllers;
using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Tests.xUnit
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            // TODO: Initialize mock and controller in constructor
            // This demonstrates xUnit's constructor injection pattern
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetProduct_WithValidId_ReturnsOkObjectResult()
        {
            // Arrange: Setup the mock to return a valid product
            var product = new Product { Id = 1, Name = "Laptop", Price = 999.99m };
            _mockProductService.Setup(s => s.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            // Act: Call GetProduct with a valid ID
            var result = await _controller.GetProduct(1);

            // Assert: Verify the result is OkObjectResult with correct product data
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, returnedProduct.Id);
            Assert.Equal("Laptop", returnedProduct.Name);
        }

        [Fact]
        public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange: Setup the mock to return null for non-existent product
            _mockProductService.Setup(s => s.GetProductByIdAsync(99))
                .ReturnsAsync((Product?)null);

            // Act: Call GetProduct with an invalid ID
            var result = await _controller.GetProduct(99);

            // Assert: Verify the result is NotFoundResult
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
