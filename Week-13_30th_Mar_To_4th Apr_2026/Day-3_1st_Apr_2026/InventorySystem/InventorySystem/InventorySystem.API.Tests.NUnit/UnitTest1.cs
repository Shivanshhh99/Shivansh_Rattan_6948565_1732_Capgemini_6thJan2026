using NUnit.Framework;
using Moq;
using InventorySystem.API.Controllers;
using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Tests.NUnit
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _mockProductService;
        private ProductController _controller;

        [SetUp]
        public void Setup()
        {
            // TODO: Initialize mock and controller in SetUp method
            // This demonstrates NUnit's [SetUp] pattern (runs before each test)
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Test]
        public async Task GetProduct_WithValidId_ReturnsOkObjectResult()
        {
            // Arrange: Setup the mock to return a valid product
            var product = new Product { Id = 1, Name = "Laptop", Price = 999.99m };
            _mockProductService.Setup(s => s.GetProductByIdAsync(1))
                .ReturnsAsync(product);

            // Act: Call GetProduct with a valid ID
            var result = await _controller.GetProduct(1);

            // Assert: Verify the result is OkObjectResult with correct product data
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (result as OkObjectResult)!;
            var returnedProduct = (okResult.Value as Product)!;
            Assert.That(returnedProduct.Id, Is.EqualTo(1));
            Assert.That(returnedProduct.Name, Is.EqualTo("Laptop"));
        }

        [Test]
        public async Task GetProduct_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange: Setup the mock to return null for non-existent product
            _mockProductService.Setup(s => s.GetProductByIdAsync(99))
                .ReturnsAsync((Product?)null);

            // Act: Call GetProduct with an invalid ID
            var result = await _controller.GetProduct(99);

            // Assert: Verify the result is NotFoundResult
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }
    }
}
