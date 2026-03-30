using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.ShippingDetail)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName");
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int customerId, List<int> productIds,
            List<int> quantities, string shippingAddress)
        {
            if (productIds == null || productIds.Count == 0)
            {
                ModelState.AddModelError("", "Add at least one product.");
                ViewBag.Customers = new SelectList(_context.Customers, "Id", "FullName");
                ViewBag.Products = _context.Products.ToList();
                return View();
            }

            var order = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            decimal total = 0;
            for (int i = 0; i < productIds.Count; i++)
            {
                var product = await _context.Products.FindAsync(productIds[i]);
                if (product == null) continue;

                int qty = quantities[i];
                var item = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = productIds[i],
                    Quantity = qty,
                    UnitPrice = product.Price
                };
                _context.OrderItems.Add(item);
                total += product.Price * qty;

                // Reduce stock
                product.StockQuantity -= qty;
            }

            order.TotalAmount = total;

            // Create shipping detail
            _context.ShippingDetails.Add(new ShippingDetail
            {
                OrderId = order.Id,
                ShippingAddress = shippingAddress,
                Status = ShippingStatus.NotShipped
            });

            await _context.SaveChangesAsync();
            TempData["Success"] = "Order placed successfully!";
            return RedirectToAction(nameof(Details), new { id = order.Id });
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Include(o => o.ShippingDetail)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();

            var vm = new OrderDetailViewModel
            {
                OrderId = order.Id,
                CustomerName = order.Customer?.FullName ?? "",
                CustomerEmail = order.Customer?.Email ?? "",
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingDetail?.ShippingAddress ?? "",
                CarrierName = order.ShippingDetail?.CarrierName ?? "",
                TrackingNumber = order.ShippingDetail?.TrackingNumber ?? "",
                ShippingStatus = order.ShippingDetail?.Status.ToString() ?? "",
                EstimatedDelivery = order.ShippingDetail?.EstimatedDelivery,
                Items = order.OrderItems.Select(oi => new OrderItemViewModel
                {
                    ProductName = oi.Product?.Name ?? "",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Subtotal = oi.Subtotal
                }).ToList()
            };
            return View(vm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null) _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Order deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}