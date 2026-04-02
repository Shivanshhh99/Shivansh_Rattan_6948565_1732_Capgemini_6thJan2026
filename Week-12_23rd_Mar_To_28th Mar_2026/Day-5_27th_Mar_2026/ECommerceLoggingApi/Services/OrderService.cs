using ECommerceLoggingApi.DTOs;
using ECommerceLoggingApi.Models;

namespace ECommerceLoggingApi.Services
{
    public class OrderService
    {
        private readonly ProductService _productService;
        private static readonly List<Order> _orders = new();
        private static int _orderCounter = 1;

        public OrderService(ProductService productService)
        {
            _productService = productService;
        }

        public Order CreateOrder(OrderRequestDto request)
        {
            var product = _productService.GetProductById(request.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            if (product.Stock < request.Quantity)
                throw new Exception("Insufficient stock.");

            // Duplicate order check
            bool duplicateOrder = _orders.Any(o =>
                o.UserId == request.UserId &&
                o.ProductId == request.ProductId &&
                o.Quantity == request.Quantity &&
                (DateTime.UtcNow - o.CreatedAt).TotalSeconds < 30);

            if (duplicateOrder)
                throw new Exception("Duplicate order detected.");

            var order = new Order
            {
                Id = _orderCounter++,
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalAmount = product.Price * request.Quantity,
                CreatedAt = DateTime.UtcNow
            };

            _orders.Add(order);
            product.Stock -= request.Quantity;

            return order;
        }

        public Order? GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }
    }
}