using Microsoft.AspNetCore.Mvc;
using InventorySystem.Services.Interfaces;

namespace InventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get a product by ID.
        /// Participants should implement this method to:
        /// - Call _productService.GetProductByIdAsync(id)
        /// - If the product is found, return 200 OK with the product details
        /// - If the product is not found, return 404 Not Found
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>200 OK with product or 404 Not Found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
