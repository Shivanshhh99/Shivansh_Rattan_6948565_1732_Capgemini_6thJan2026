using ECommerce.API.Models;

namespace ECommerce.API.Services;

public interface IOrderService
{
    Task PlaceOrderAsync(Order order);
    Task<List<Order>> GetOrdersAsync();
}