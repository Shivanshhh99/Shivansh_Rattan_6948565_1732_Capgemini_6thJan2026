using ECommerceOMS.Data;
using ECommerceOMS.Models;
using ECommerceOMS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context) => _context = context;

        // Simple session-based admin check
        private bool IsAdmin() => HttpContext.Session.GetString("IsAdmin") == "true";

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Hardcoded for assessment; use Identity in production
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction(nameof(Dashboard));
            }
            ViewBag.Error = "Invalid credentials.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            // Top 5 selling products
            var topProducts = await _context.OrderItems
                .GroupBy(oi => oi.Product!.Name)
                .Select(g => new TopProductViewModel
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(x => x.Quantity),
                    Revenue = g.Sum(x => x.Quantity * x.UnitPrice)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToListAsync();

            // Pending orders (not shipped yet)
            var pending = await _context.Orders
                .Include(o => o.Customer)
                .Where(o => o.Status == OrderStatus.Pending || o.Status == OrderStatus.Processing)
                .OrderBy(o => o.OrderDate)
                .Select(o => new PendingOrderViewModel
                {
                    OrderId = o.Id,
                    CustomerName = o.Customer!.FullName,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status.ToString()
                })
                .ToListAsync();

            // Customer summaries
            var summaries = await _context.Customers
                .Include(c => c.Orders)
                .Select(c => new CustomerSummaryViewModel
                {
                    CustomerName = c.FullName,
                    Email = c.Email,
                    TotalOrders = c.Orders.Count,
                    TotalSpent = c.Orders.Sum(o => o.TotalAmount)
                })
                .OrderByDescending(c => c.TotalSpent)
                .ToListAsync();

            var vm = new DashboardViewModel
            {
                TopProducts = topProducts,
                PendingOrders = pending,
                CustomerSummaries = summaries,
                TotalOrders = await _context.Orders.CountAsync(),
                TotalCustomers = await _context.Customers.CountAsync(),
                TotalProducts = await _context.Products.CountAsync(),
                TotalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount)
            };

            return View(vm);
        }

        // Update shipping status
        [HttpPost]
        public async Task<IActionResult> UpdateShipping(int orderId, ShippingStatus status,
            string carrier, string tracking, DateTime? estimatedDelivery)
        {
            if (!IsAdmin()) return RedirectToAction("Login");

            var shipping = await _context.ShippingDetails
                .FirstOrDefaultAsync(s => s.OrderId == orderId);

            if (shipping != null)
            {
                shipping.Status = status;
                shipping.CarrierName = carrier;
                shipping.TrackingNumber = tracking;
                shipping.EstimatedDelivery = estimatedDelivery;

                // Also update order status
                var order = await _context.Orders.FindAsync(orderId);
                if (order != null)
                    order.Status = status == ShippingStatus.Delivered
                        ? OrderStatus.Delivered : OrderStatus.Shipped;

                await _context.SaveChangesAsync();
                TempData["Success"] = "Shipping updated!";
            }
            return RedirectToAction(nameof(Dashboard));
        }
    }
}