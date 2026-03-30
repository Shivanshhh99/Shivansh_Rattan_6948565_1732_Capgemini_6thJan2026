using Microsoft.AspNetCore.Mvc;
using EmployeeManagementPortal.Filters;
using EmployeeManagementPortal.Models;

namespace EmployeeManagementPortal.Controllers
{
    [ServiceFilter(typeof(LogActionFilter))]   // Logging on every HR action
    public class HRController : Controller
    {
        private IActionResult RedirectIfNotLoggedIn()
        {
            if (HttpContext.Session.GetString("LoggedInUser") == null)
                return RedirectToAction("Login", "Account");
            return null!;
        }

        // GET: /HR/Index
        public IActionResult Index()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            ViewBag.TotalEmployees = EmployeeController.Employees.Count;
            ViewBag.ActiveCount = EmployeeController.Employees.Count(e => e.Status == "Active");
            ViewBag.InactiveCount = EmployeeController.Employees.Count(e => e.Status == "Inactive");
            ViewBag.Departments = EmployeeController.Employees
                                        .GroupBy(e => e.Department)
                                        .Select(g => new { Dept = g.Key, Count = g.Count() })
                                        .ToList();
            return View();
        }

        // GET: /HR/EmployeeList
        public IActionResult EmployeeList()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            return View(EmployeeController.Employees);
        }

        // GET: /HR/Reports
        public IActionResult Reports()
        {
            var redirect = RedirectIfNotLoggedIn();
            if (redirect != null) return redirect;

            // UNCOMMENT to test exception filter:
            // throw new Exception("Test exception from HRController.Reports!");

            var deptGroups = EmployeeController.Employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count(),
                    AvgAge = g.Average(e => e.Age),
                    Active = g.Count(e => e.Status == "Active"),
                    Inactive = g.Count(e => e.Status == "Inactive")
                }).ToList();

            ViewBag.DeptGroups = deptGroups;
            ViewBag.TotalEmployees = EmployeeController.Employees.Count;
            ViewBag.OldestEmployee = EmployeeController.Employees.OrderByDescending(e => e.Age).FirstOrDefault();
            ViewBag.NewestJoin = EmployeeController.Employees.OrderByDescending(e => e.JoinedOn).FirstOrDefault();

            return View();
        }
    }
}