using InventorySystem.Services.Models;

namespace InventorySystem.Services.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Get a product by its ID asynchronously.
        /// </summary>
        /// <param name="id">The product ID</param>
        /// <returns>Product object if found, null otherwise</returns>
        Task<Product?> GetProductByIdAsync(int id);
    }
}
