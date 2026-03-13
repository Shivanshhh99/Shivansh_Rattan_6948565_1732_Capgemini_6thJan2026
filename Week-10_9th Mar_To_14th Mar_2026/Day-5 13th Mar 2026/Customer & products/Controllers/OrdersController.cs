using Customer___products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class OrdersController : Controller
{
    private readonly AppDBContext _context;

    public OrdersController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var orders = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .ToList();

        return View(orders);
    }

    public IActionResult Create()
    {
        ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name");
        ViewBag.Products = new SelectList(_context.Products, "Id", "Name");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var order = _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Product)
            .FirstOrDefault(o => o.Id == id);

        return View(order);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var order = _context.Orders.Find(id);
        _context.Orders.Remove(order);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}