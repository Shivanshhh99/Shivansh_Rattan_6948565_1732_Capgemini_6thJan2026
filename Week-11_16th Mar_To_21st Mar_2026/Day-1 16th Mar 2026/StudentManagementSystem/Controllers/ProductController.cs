using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Filters;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))] // Apply logging to this controller only
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 75000, Description = "High-performance laptop", Category = "Electronics", CreatedAt = DateTime.Now },
            new Product { Id = 2, Name = "Desk Chair", Price = 12000, Description = "Ergonomic office chair", Category = "Furniture", CreatedAt = DateTime.Now },
            new Product { Id = 3, Name = "Notebook", Price = 150, Description = "A4 ruled notebook", Category = "Stationery", CreatedAt = DateTime.Now },
        };
        private static int _nextId = 4;

        // GET: /Product/Index
        public IActionResult Index()
        {
            // UNCOMMENT the line below to test exception filter:
            // throw new Exception("Test exception from ProductController.Index!");

            return View(_products);
        }

        // GET: /Product/Details/1
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = _nextId++;
                product.CreatedAt = DateTime.Now;
                _products.Add(product);

                TempData["SuccessMessage"] = $"Product '{product.Name}' created successfully!";
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}