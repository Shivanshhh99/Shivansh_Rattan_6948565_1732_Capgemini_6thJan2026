using InventorySystem.Services.Models;

namespace InventorySystem.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Place an order asynchronously.
        /// </summary>
        /// <param name="order">The order to place</param>
        /// <returns>true if order was placed successfully, false otherwise</returns>
        Task<bool> PlaceOrderAsync(Order order);
    }
}
