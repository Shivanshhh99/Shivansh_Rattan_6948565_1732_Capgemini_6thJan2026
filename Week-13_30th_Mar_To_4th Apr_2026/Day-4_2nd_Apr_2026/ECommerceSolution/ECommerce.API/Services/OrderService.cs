using ECommerce.API.Models;
using ECommerce.API.Repositories;

namespace ECommerce.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repo;

    public OrderService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task PlaceOrderAsync(Order order)
    {
        order.OrderDate = DateTime.Now;
        await _repo.AddAsync(order);
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _repo.GetAllAsync();
    }
}