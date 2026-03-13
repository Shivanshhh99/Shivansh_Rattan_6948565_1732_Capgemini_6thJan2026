using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CompanyEmployees.Models;

namespace CompanyEmployees.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyDbContext _context;

        public EmployeesController(CompanyDbContext context)
        {
            _context = context;
        }

        // CREATE EMPLOYEE (GET)
        public IActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName");
            return View();
        }

        // CREATE EMPLOYEE (POST)
        [HttpPost]
        public IActionResult Create(EmployeeModel emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("Index", "Companies");
            }

            ViewBag.CompanyId = new SelectList(_context.Companies, "CompanyId", "CompanyName", emp.CompanyId);
            return View(emp);
        }
    }
}