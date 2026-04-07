using ECommerce.API.Models;
using ECommerce.API.Repositories;

namespace ECommerce.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task AddProductAsync(Product product)
    {
        await _repo.AddAsync(product);
    }
}