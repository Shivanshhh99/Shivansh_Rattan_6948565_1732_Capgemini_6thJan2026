using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementPortal.Controllers
{
    public class AccountController : Controller
    {
        private const string SessionKeyUser = "LoggedInUser";
        private const string SessionKeyRole = "UserRole";

        // GET: /Account/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString(SessionKeyUser) != null)
                return RedirectToAction("Dashboard");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString(SessionKeyUser, username);
                HttpContext.Session.SetString(SessionKeyRole, "Administrator");
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid credentials. Use admin / admin123.";
            return View();
        }

        // GET: /Account/Dashboard
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString(SessionKeyUser);
            if (user == null) return RedirectToAction("Login");

            ViewBag.Username = user;
            ViewBag.Role = HttpContext.Session.GetString(SessionKeyRole);
            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}