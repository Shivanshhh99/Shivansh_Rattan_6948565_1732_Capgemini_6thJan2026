using ECommerce.API.Models;

namespace ECommerce.API.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task AddAsync(Order order);
}