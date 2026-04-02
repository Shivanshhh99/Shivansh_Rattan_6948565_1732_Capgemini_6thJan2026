using ECommerceLoggingApi.Models;

namespace ECommerceLoggingApi.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 65000, Stock = 10 },
            new Product { Id = 2, Name = "Mobile", Price = 25000, Stock = 15 },
            new Product { Id = 3, Name = "Headphones", Price = 3000, Stock = 20 }
        };

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
    }
}