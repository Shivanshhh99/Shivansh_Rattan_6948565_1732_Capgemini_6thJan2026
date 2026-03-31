using Microsoft.AspNetCore.Mvc;
using CompanyManager.Data;
using CompanyManager.Models;
using System.Linq;

namespace CompanyManager.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}