using ECommerceOMS.Data;
using ECommerceOMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceOMS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AppDbContext _context;
        public CustomersController(AppDbContext context) => _context = context;

        public async Task<IActionResult> Index()
{
    var customers = await _context.Customers
        .Include(c => c.Orders)
        .ToListAsync();
    return View(customers);
}

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) return View(customer);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Customer created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();
            if (!ModelState.IsValid) return View(customer);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Customer updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                        .ThenInclude(oi => oi.Product)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.ShippingDetail)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) return NotFound();
            return View(customer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null) _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Customer deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}