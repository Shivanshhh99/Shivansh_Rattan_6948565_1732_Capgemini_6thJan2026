using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Data;
using CompanyManager.Models;

namespace CompanyManager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees
                .Include(e => e.Department)
                .ToList();

            return View(employees);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Projects = _context.Projects.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee, int[] selectedProjects)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();

                if (selectedProjects != null)
                {
                    foreach (var projectId in selectedProjects)
                    {
                        EmployeeProject ep = new EmployeeProject
                        {
                            EmployeeId = employee.EmployeeId,
                            ProjectId = projectId,
                            AssignedDate = DateTime.Now
                        };

                        _context.EmployeeProjects.Add(ep);
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            // If validation fails, reload dropdown data
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Projects = _context.Projects.ToList();

            return View(employee);
        }

        public IActionResult ProjectsByEmployee(int id)
        {
            var projects = _context.EmployeeProjects
                .Where(ep => ep.EmployeeId == id)
                .Select(ep => ep.Project)
                .ToList();

            return View(projects);
        }
    }
}