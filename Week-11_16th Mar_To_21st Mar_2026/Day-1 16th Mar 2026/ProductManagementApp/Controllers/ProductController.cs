using Microsoft.AspNetCore.Mvc;
using ProductManagementApp.Models;
using ProductManagementApp.Filters;

namespace ProductManagementApp.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]
    public class ProductController : Controller
    {
        public IActionResult Index(bool triggerError = false)
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 55000 },
                new Product { Id = 2, Name = "Mobile", Price = 20000 },
                new Product { Id = 3, Name = "Headphones", Price = 3000 }
            };

            // 🔥 Dynamic exception trigger
            if (triggerError)
            {
                throw new Exception("Test exception triggered dynamically");
            }

            return View(products);
        }
    }
}