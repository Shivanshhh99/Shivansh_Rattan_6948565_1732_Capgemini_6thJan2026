using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;

namespace InventorySystem.Services.Services
{
    public class ProductService : IProductService
    {
        /// <summary>
        /// Participants should implement this method to:
        /// - Find a product by ID from a data source (database, in-memory, etc.)
        /// - Return the Product if found
        /// - Return null if not found
        /// </summary>
        public Task<Product?> GetProductByIdAsync(int id)
        {
            // Sample in-memory data for demonstration
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 999.99m },
                new Product { Id = 2, Name = "Mouse", Price = 29.99m },
                new Product { Id = 3, Name = "Keyboard", Price = 79.99m }
            };

            var product = products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult<Product?>(product);
        }
    }
}
