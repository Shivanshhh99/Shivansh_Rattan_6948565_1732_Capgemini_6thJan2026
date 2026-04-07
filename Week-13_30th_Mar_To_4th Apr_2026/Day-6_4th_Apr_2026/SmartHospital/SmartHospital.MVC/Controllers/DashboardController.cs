using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Controllers;

public class DashboardController : Controller
{
    private readonly IApiService _api;

    public DashboardController(IApiService api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString(SessionHelper.JwtToken);
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        var userId = HttpContext.Session.GetInt32(SessionHelper.UserId);

        ViewBag.Role = role;
        ViewBag.UserName = HttpContext.Session.GetString(SessionHelper.UserName);
        ViewBag.UserId = userId;

        var departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
        ViewBag.DepartmentCount = departments?.Count ?? 0;

        var doctors = await _api.GetAsync<List<dynamic>>("api/doctors", token);
        ViewBag.DoctorCount = doctors?.Count ?? 0;

        if (role == "Patient" && userId.HasValue)
        {
            var appointments = await _api.GetAsync<List<dynamic>>($"api/appointments/patient/{userId}", token);
            ViewBag.AppointmentCount = appointments?.Count ?? 0;
            ViewBag.Appointments = appointments?.Take(5).ToList();

            var bills = await _api.GetAsync<List<JsonElement>>($"api/billing/patient/{userId}", token);
            ViewBag.BillCount = bills?.Count ?? 0;
            ViewBag.UnpaidBillCount = bills?.Count(b =>
                b.TryGetProperty("paymentStatus", out var status) &&
                string.Equals(status.GetString(), "Unpaid", StringComparison.OrdinalIgnoreCase)) ?? 0;
        }

        return View();
    }
}