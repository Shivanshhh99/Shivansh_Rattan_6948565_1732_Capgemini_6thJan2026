using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private const string SessionKeyUser = "LoggedInUser";

        // GET: /Account/Login
        public IActionResult Login()
        {
            // If already logged in, skip login page
            if (HttpContext.Session.GetString(SessionKeyUser) != null)
                return RedirectToAction("Dashboard");

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                HttpContext.Session.SetString(SessionKeyUser, username);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password. Try admin / 123.";
            return View();
        }

        // GET: /Account/Dashboard
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString(SessionKeyUser);

            if (user == null)
                return RedirectToAction("Login"); // Not logged in

            ViewBag.Username = user;
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