using InventorySystem.Services.Interfaces;
using InventorySystem.Services.Models;

namespace InventorySystem.Services.Services
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// Participants should implement this method to:
        /// - Validate the order (check if items are valid, quantities are positive, etc.)
        /// - If invalid, return false
        /// - If valid, save the order to the database
        /// - Return true if successful, false otherwise
        /// </summary>
        public Task<bool> PlaceOrderAsync(Order order)
        {
            // Validate order
            if (order == null || order.Items == null || order.Items.Count == 0)
            {
                return Task.FromResult(false);
            }

            // Validate all items have positive quantities
            if (order.Items.Any(item => item.Quantity <= 0))
            {
                return Task.FromResult(false);
            }

            // In a real scenario, you would save to a database here
            // For this demo, we'll just validate and return true
            return Task.FromResult(true);
        }
    }
}
