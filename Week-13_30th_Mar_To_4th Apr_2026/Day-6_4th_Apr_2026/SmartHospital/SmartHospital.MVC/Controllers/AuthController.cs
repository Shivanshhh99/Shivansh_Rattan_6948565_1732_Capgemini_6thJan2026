using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Controllers;

public class AuthController : Controller
{
    private readonly IApiService _api;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IApiService api, ILogger<AuthController> logger)
    {
        _api = api;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (HttpContext.Session.GetString(SessionHelper.JwtToken) != null)
            return RedirectToAction("Index", "Dashboard");

        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var response = await _api.PostAsync<AuthResponseViewModel>("api/auth/login", new
        {
            model.Email,
            model.Password
        });

        if (response is null)
        {
            ModelState.AddModelError("", "Invalid email or password. Please try again.");
            return View(model);
        }

        HttpContext.Session.SetString(SessionHelper.JwtToken, response.Token);
        HttpContext.Session.SetString(SessionHelper.UserRole, response.Role);
        HttpContext.Session.SetInt32(SessionHelper.UserId, response.UserId);
        HttpContext.Session.SetString(SessionHelper.UserName, response.FullName);
        HttpContext.Session.SetString(SessionHelper.UserEmail, response.Email);

        _logger.LogInformation("User {Email} logged in with role {Role}", response.Email, response.Role);

        // Redirect based on role
        if (response.Role == "Admin")
            return RedirectToAction("Dashboard", "Admin");

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "Dashboard");
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var response = await _api.PostAsync<AuthResponseViewModel>("api/auth/register", new
        {
            model.FullName,
            model.Email,
            model.Password,
            model.ConfirmPassword,
            Role = "Patient"
        });

        if (response is null)
        {
            ModelState.AddModelError("", "Registration failed. Email may already be in use.");
            return View(model);
        }

        TempData["Success"] = "Registration successful! Please login.";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AdminLogin()
    {
        var token = HttpContext.Session.GetString(SessionHelper.JwtToken);
        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        
        if (token != null && role == "Admin")
            return RedirectToAction("Dashboard", "Admin");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AdminLogin(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var response = await _api.PostAsync<AuthResponseViewModel>("api/auth/login", new
        {
            model.Email,
            model.Password
        });

        if (response is null)
        {
            ModelState.AddModelError("", "Invalid admin credentials. Please try again.");
            return View(model);
        }

        if (response.Role != "Admin")
        {
            ModelState.AddModelError("", "This account is not an admin account. Please use the regular login.");
            return View(model);
        }

        HttpContext.Session.SetString(SessionHelper.JwtToken, response.Token);
        HttpContext.Session.SetString(SessionHelper.UserRole, response.Role);
        HttpContext.Session.SetInt32(SessionHelper.UserId, response.UserId);
        HttpContext.Session.SetString(SessionHelper.UserName, response.FullName);
        HttpContext.Session.SetString(SessionHelper.UserEmail, response.Email);

        _logger.LogInformation("Admin {Email} logged in", response.Email);
        return RedirectToAction("Dashboard", "Admin");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}