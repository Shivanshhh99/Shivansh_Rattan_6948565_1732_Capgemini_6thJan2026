using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var startTime = DateTime.UtcNow;

            var products = await _productService.GetAllProductsAsync();

            var endTime = DateTime.UtcNow;
            var responseTime = (endTime - startTime).TotalMilliseconds;

            return Ok(new
            {
                message = "Products fetched successfully",
                responseTimeInMs = responseTime,
                data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var startTime = DateTime.UtcNow;

            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = $"Product with ID {id} not found"
                });
            }

            var endTime = DateTime.UtcNow;
            var responseTime = (endTime - startTime).TotalMilliseconds;

            return Ok(new
            {
                message = "Product fetched successfully",
                responseTimeInMs = responseTime,
                data = product
            });
        }

        [HttpDelete("cache")]
        public IActionResult ClearCache()
        {
            _productService.ClearProductCache();

            return Ok(new
            {
                message = "Product cache cleared successfully"
            });
        }
    }
}