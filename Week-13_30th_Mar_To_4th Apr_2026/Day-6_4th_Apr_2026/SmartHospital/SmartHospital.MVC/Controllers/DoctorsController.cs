using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Controllers;

public class DoctorsController : Controller
{
    private readonly IApiService _api;

    public DoctorsController(IApiService api) => _api = api;

    private string? GetToken() => HttpContext.Session.GetString(SessionHelper.JwtToken);

    public async Task<IActionResult> Index(int? departmentId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var endpoint = departmentId.HasValue
            ? $"api/doctors/department/{departmentId}"
            : "api/doctors";

        var doctors = await _api.GetAsync<List<JsonElement>>(endpoint, token);
        var departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);

        ViewBag.Departments = departments;
        ViewBag.SelectedDepartment = departmentId;
        ViewBag.Role = HttpContext.Session.GetString(SessionHelper.UserRole);
        return View(doctors);
    }

    public async Task<IActionResult> Details(int id)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var doctor = await _api.GetAsync<JsonElement>($"api/doctors/{id}", token);
        return View(doctor);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        if (role != "Admin") return Forbid();

        var token = GetToken();
        var departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);
        ViewBag.Departments = departments;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDoctorViewModel model)
    {
        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        if (role != "Admin") return Forbid();

        if (!ModelState.IsValid)
        {
            var token2 = GetToken();
            ViewBag.Departments = await _api.GetAsync<List<JsonElement>>("api/departments", token2);
            return View(model);
        }

        var token = GetToken();
        var result = await _api.PostAsync<JsonElement>("api/doctors", new
        {
            model.FullName,
            model.Email,
            model.Password,
            model.DepartmentId,
            model.Specialization,
            model.ExperienceYears,
            model.Availability
        }, token);

        if (result.ValueKind == JsonValueKind.Undefined)
        {
            ModelState.AddModelError("", "Failed to create doctor. Email may already exist.");
            ViewBag.Departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);
            return View(model);
        }

        TempData["Success"] = "Doctor created successfully!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        if (role != "Admin") return Forbid();

        var token = GetToken();
        await _api.DeleteAsync($"api/doctors/{id}", token);

        TempData["Success"] = "Doctor deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}