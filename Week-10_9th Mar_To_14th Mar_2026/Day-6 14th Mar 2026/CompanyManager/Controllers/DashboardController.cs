using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Data;

namespace CompanyManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Projects
                .Include(p => p.EmployeeProjects)
                .ThenInclude(ep => ep.Employee)
                .ThenInclude(e => e.Department)
                .ToList();

            return View(data);
        }

        public IActionResult EmployeesPerDepartment()
        {
            var result = _context.Departments
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    EmployeeCount = d.Employees.Count()
                }).ToList();

            return View(result);
        }
    }
}