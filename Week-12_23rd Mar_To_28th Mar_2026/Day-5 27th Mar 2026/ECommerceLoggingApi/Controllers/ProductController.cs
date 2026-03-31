using ECommerceLoggingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductService _productService;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            _logger.LogInformation("Product fetch request received.");

            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            _logger.LogInformation("Product fetch request for ProductId: {ProductId}", id);

            var product = _productService.GetProductById(id);

            if (product == null)
            {
                _logger.LogWarning("Product not found. ProductId: {ProductId}", id);
                return NotFound("Product not found");
            }

            return Ok(product);
        }
    }
}