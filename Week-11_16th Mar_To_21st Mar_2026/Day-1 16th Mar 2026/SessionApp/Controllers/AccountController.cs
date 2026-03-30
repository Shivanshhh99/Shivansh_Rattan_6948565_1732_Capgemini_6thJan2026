using Microsoft.AspNetCore.Mvc;

namespace SessionApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "123")
            {
                // ✅ Store in session
                HttpContext.Session.SetString("User", username);

                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password!";
            return View();
        }

        // Dashboard
        public IActionResult Dashboard()
        {
            var user = HttpContext.Session.GetString("User");

            // ✅ Protect route
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Username = user;
            return View();
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}