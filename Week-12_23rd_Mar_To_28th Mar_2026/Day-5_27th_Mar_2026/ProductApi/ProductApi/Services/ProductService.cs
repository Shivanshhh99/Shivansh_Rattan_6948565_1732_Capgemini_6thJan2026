using Microsoft.Extensions.Caching.Memory;
using ProductApi.DTOs;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ProductService> _logger;

        private const string ProductListCacheKey = "product_list";

        public ProductService(
            IProductRepository repository,
            IMemoryCache cache,
            ILogger<ProductService> logger)
        {
            _repository = repository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            if (_cache.TryGetValue(ProductListCacheKey, out List<ProductDto>? cachedProducts))
            {
                _logger.LogInformation("Products fetched from CACHE.");
                return cachedProducts!;
            }

            _logger.LogInformation("Products fetched from DATABASE.");

            var products = await _repository.GetAllProductsAsync();

            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price
            }).ToList();

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                .SetPriority(CacheItemPriority.High);

            _cache.Set(ProductListCacheKey, productDtos, cacheOptions);

            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            string cacheKey = $"product_{id}";

            if (_cache.TryGetValue(cacheKey, out ProductDto? cachedProduct))
            {
                _logger.LogInformation("Product {ProductId} fetched from CACHE.", id);
                return cachedProduct;
            }

            _logger.LogInformation("Product {ProductId} fetched from DATABASE.", id);

            var product = await _repository.GetProductByIdAsync(id);

            if (product == null)
                return null;

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, productDto, cacheOptions);

            return productDto;
        }

        public void ClearProductCache()
        {
            _cache.Remove(ProductListCacheKey);

            for (int i = 1; i <= 100; i++)
            {
                _cache.Remove($"product_{i}");
            }

            _logger.LogInformation("Product cache cleared.");
        }
    }
}