using Microsoft.AspNetCore.Mvc;
using CompanyManager.Data;
using CompanyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var projects = _context.Projects.ToList();
            return View(projects);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project, int[] selectedEmployees)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();

                if (selectedEmployees != null)
                {
                    foreach (var empId in selectedEmployees)
                    {
                        _context.EmployeeProjects.Add(new EmployeeProject
                        {
                            EmployeeId = empId,
                            ProjectId = project.ProjectId,
                            AssignedDate = DateTime.Now
                        });
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Employees = _context.Employees.ToList();
            return View(project);
        }

        public IActionResult EmployeesByProject(int id)
        {
            var employees = _context.EmployeeProjects
                .Where(ep => ep.ProjectId == id)
                .Select(ep => ep.Employee)
                .ToList();

            return View(employees);
        }
    }
}