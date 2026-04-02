using ProductHub.API.DTOs;
using ProductHub.API.Models;

namespace ProductHub.API.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(ProductDto dto);
    Task<bool> UpdateProductAsync(int id, ProductDto dto);
    Task<bool> DeleteProductAsync(int id);
}