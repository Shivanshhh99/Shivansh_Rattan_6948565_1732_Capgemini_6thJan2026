using ECommerce.API.Models;

namespace ECommerce.API.Services;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
}