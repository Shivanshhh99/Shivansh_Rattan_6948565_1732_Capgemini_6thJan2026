using CompanyEmployees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Model_level_Validation.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly CompanyDbContext _context;

        public CompaniesController(CompanyDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public IActionResult Index()
        {
            var companies = _context.Companies
                .Include(c => c.employees)
                .ToList();

            return View(companies);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(company);
            }

            _context.Companies.Add(company);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
    
}