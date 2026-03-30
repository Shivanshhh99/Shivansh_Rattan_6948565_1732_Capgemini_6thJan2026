using Microsoft.AspNetCore.Mvc;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Controllers
{
    public class EmployeeController : Controller
    {
        // In-memory data store (static = persists for app lifetime)
        public static List<Employee> Employees = new()
        {
            new Employee { Id=1, Name="Priya Sharma",   Age=29, Email="priya@corp.com",   Department="Engineering",   JobTitle="Software Engineer",    Status="Active",   JoinedOn=DateTime.Now.AddMonths(-18) },
            new Employee { Id=2, Name="Rahul Mehta",    Age=34, Email="rahul@corp.com",   Department="HR",            JobTitle="HR Manager",           Status="Active",   JoinedOn=DateTime.Now.AddMonths(-36) },
            new Employee { Id=3, Name="Ananya Gupta",   Age=27, Email="ananya@corp.com",  Department="Finance",       JobTitle="Finance Analyst",      Status="Active",   JoinedOn=DateTime.Now.AddMonths(-9)  },
            new Employee { Id=4, Name="Vikram Singh",   Age=42, Email="vikram@corp.com",  Department="Operations",    JobTitle="Operations Lead",      Status="Inactive", JoinedOn=DateTime.Now.AddMonths(-60) },
        };
        private static int _nextId = 5;

        // GET: /Employee/Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: /Employee/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Employee emp)
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                emp.Id = _nextId++;
                emp.JoinedOn = DateTime.Now;
                emp.Status = "Active";
                Employees.Add(emp);

                TempData["SuccessMessage"] = $"Employee '{emp.Name}' registered successfully!";
                return RedirectToAction("Details", new { id = emp.Id });
            }

            return View(emp);
        }

        // GET: /Employee/Details/1
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
                return RedirectToAction("Login", "Account");

            var emp = Employees.FirstOrDefault(e => e.Id == id);
            if (emp == null) return NotFound();

            return View(emp);
        }
    }
}