using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Controllers;

public class AppointmentsController : Controller
{
    private readonly IApiService _api;

    public AppointmentsController(IApiService api) => _api = api;

    private string? GetToken() => HttpContext.Session.GetString(SessionHelper.JwtToken);

    public async Task<IActionResult> Index()
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        var userId = HttpContext.Session.GetInt32(SessionHelper.UserId);

        List<JsonElement>? appointments = null;

        if (role == "Patient")
            appointments = await _api.GetAsync<List<JsonElement>>($"api/appointments/patient/{userId}", token);
        else if (role == "Admin")
            appointments = await _api.GetAsync<List<JsonElement>>("api/appointments", token);
        else if (role == "Doctor")
        {
            // Fetch doctor record for this user
            var allDoctors = await _api.GetAsync<List<JsonElement>>("api/doctors", token);
            var myDoctor = allDoctors?.FirstOrDefault(d => d.GetProperty("userId").GetInt32() == userId);
            if (myDoctor.HasValue)
            {
                var doctorId = myDoctor.Value.GetProperty("doctorId").GetInt32();
                appointments = await _api.GetAsync<List<JsonElement>>($"api/appointments/doctor/{doctorId}", token);
            }
        }

        ViewBag.Role = role;
        return View(appointments);
    }

    [HttpGet]
    public async Task<IActionResult> Book(int? departmentId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        if (role != "Patient") return Forbid();

        var departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);
        ViewBag.Departments = departments;

        List<JsonElement>? doctors = null;
        if (departmentId.HasValue)
        {
            doctors = await _api.GetAsync<List<JsonElement>>($"api/doctors/department/{departmentId}", token);
            ViewBag.SelectedDepartment = departmentId;
        }
        else
        {
            doctors = await _api.GetAsync<List<JsonElement>>("api/doctors", token);
        }

        ViewBag.Doctors = doctors;

        var model = new BookAppointmentViewModel
        {
            PatientId = HttpContext.Session.GetInt32(SessionHelper.UserId) ?? 0,
            FilterDepartmentId = departmentId
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(BookAppointmentViewModel model)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        if (!ModelState.IsValid)
        {
            var departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);
            var doctors = await _api.GetAsync<List<JsonElement>>("api/doctors", token);
            ViewBag.Departments = departments;
            ViewBag.Doctors = doctors;
            return View(model);
        }

        var result = await _api.PostAsync<JsonElement>("api/appointments", new
        {
            model.PatientId,
            model.DoctorId,
            AppointmentDate = model.AppointmentDate.ToUniversalTime()
        }, token);

        if (result.ValueKind == JsonValueKind.Undefined)
        {
            ModelState.AddModelError("", "Booking failed. The doctor may already be booked at this time.");
            var departments = await _api.GetAsync<List<JsonElement>>("api/departments", token);
            var doctors = await _api.GetAsync<List<JsonElement>>("api/doctors", token);
            ViewBag.Departments = departments;
            ViewBag.Doctors = doctors;
            return View(model);
        }

        TempData["Success"] = "Appointment booked successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var appointment = await _api.GetAsync<JsonElement>($"api/appointments/{id}", token);
        return View(appointment);
    }
}