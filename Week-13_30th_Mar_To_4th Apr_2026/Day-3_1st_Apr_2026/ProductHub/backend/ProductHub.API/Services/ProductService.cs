using ProductHub.API.DTOs;
using ProductHub.API.Models;
using ProductHub.API.Repositories;

namespace ProductHub.API.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<Product> AddProductAsync(ProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category
        };

        await _productRepository.AddAsync(product);
        return product;
    }

    public async Task<bool> UpdateProductAsync(int id, ProductDto dto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null)
            return false;

        existingProduct.Name = dto.Name;
        existingProduct.Price = dto.Price;
        existingProduct.Category = dto.Category;

        await _productRepository.UpdateAsync(existingProduct);
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null)
            return false;

        await _productRepository.DeleteAsync(existingProduct);
        return true;
    }
}