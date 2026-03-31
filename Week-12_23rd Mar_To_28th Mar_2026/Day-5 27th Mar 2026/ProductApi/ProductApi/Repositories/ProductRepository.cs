using ProductApi.Models;

namespace ProductApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 75000, Stock = 10 },
            new Product { Id = 2, Name = "Mobile", Category = "Electronics", Price = 30000, Stock = 25 },
            new Product { Id = 3, Name = "Tablet", Category = "Electronics", Price = 25000, Stock = 15 },
            new Product { Id = 4, Name = "Headphones", Category = "Accessories", Price = 5000, Stock = 40 },
            new Product { Id = 5, Name = "Smart Watch", Category = "Wearables", Price = 12000, Stock = 18 }
        };

        public async Task<List<Product>> GetAllProductsAsync()
        {
            // Simulate heavy DB call
            await Task.Delay(3000);
            return _products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            await Task.Delay(1500);
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}