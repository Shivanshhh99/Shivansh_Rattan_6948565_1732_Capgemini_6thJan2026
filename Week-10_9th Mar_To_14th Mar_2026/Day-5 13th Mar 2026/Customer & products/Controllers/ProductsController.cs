using Customer___products.Models;
using Microsoft.AspNetCore.Mvc;

public class ProductsController : Controller
{
    private readonly AppDBContext _context;

    public ProductsController(AppDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Products.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Product saved successfully!";

            return RedirectToAction("Index");
        }

        return View(product);
    }
    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _context.Products.Find(id);
        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}