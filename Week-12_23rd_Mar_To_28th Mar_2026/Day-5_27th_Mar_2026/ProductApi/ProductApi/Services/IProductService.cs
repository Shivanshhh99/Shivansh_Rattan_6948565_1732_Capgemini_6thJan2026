using ProductApi.DTOs;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        void ClearProductCache();
    }
}