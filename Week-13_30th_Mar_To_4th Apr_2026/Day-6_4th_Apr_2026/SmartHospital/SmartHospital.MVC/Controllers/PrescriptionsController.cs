using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace SmartHospital.MVC.Controllers;

public class PrescriptionsController : Controller
{
    private readonly IApiService _api;
    private readonly ILogger<PrescriptionsController> _logger;

    public PrescriptionsController(IApiService api, ILogger<PrescriptionsController> logger)
    {
        _api = api;
        _logger = logger;
    }

    private string? GetToken() => HttpContext.Session.GetString(SessionHelper.JwtToken);
    private string? GetUserRole() => HttpContext.Session.GetString(SessionHelper.UserRole);
    private int? GetUserId() => HttpContext.Session.GetInt32(SessionHelper.UserId);

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = GetUserRole();
        var userId = GetUserId();
        ViewBag.Role = role;

        try
        {
            if (role == "Patient" && userId.HasValue)
            {
                var patientPrescriptions = await _api.GetAsync<List<JsonElement>>($"api/prescriptions/patient/{userId.Value}", token);
                return View(patientPrescriptions ?? new List<JsonElement>());
            }

            if (role == "Admin")
            {
                var allPrescriptions = await _api.GetAsync<List<JsonElement>>("api/prescriptions", token);
                return View(allPrescriptions ?? new List<JsonElement>());
            }

            if (role == "Doctor" && userId.HasValue)
            {
                var allDoctors = await _api.GetAsync<List<JsonElement>>("api/doctors", token);
                var myDoctor = allDoctors?.FirstOrDefault(d => d.GetProperty("userId").GetInt32() == userId.Value);
                if (!myDoctor.HasValue)
                {
                    return View(new List<JsonElement>());
                }

                var doctorId = myDoctor.Value.GetProperty("doctorId").GetInt32();
                var appointments = await _api.GetAsync<List<JsonElement>>($"api/appointments/doctor/{doctorId}", token);
                var prescriptions = new List<JsonElement>();
                foreach (var appt in appointments ?? new List<JsonElement>())
                {
                    if (!appt.TryGetProperty("hasPrescription", out var hasPrescriptionProp) || !hasPrescriptionProp.GetBoolean())
                    {
                        continue;
                    }

                    var apptId = appt.GetProperty("appointmentId").GetInt32();
                    var prescription = await _api.GetAsync<JsonElement>($"api/prescriptions/appointment/{apptId}", token);
                    if (prescription.ValueKind != JsonValueKind.Undefined && prescription.ValueKind != JsonValueKind.Null)
                    {
                        prescriptions.Add(prescription);
                    }
                }

                return View(prescriptions);
            }

            return Forbid();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching prescriptions");
            TempData["ErrorMessage"] = "Failed to load prescriptions";
            return View(new List<JsonElement>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int appointmentId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        try
        {
            var prescription = await _api.GetAsync<JsonElement>($"api/prescriptions/appointment/{appointmentId}", token);
            if (prescription.ValueKind == JsonValueKind.Null)
                return NotFound();

            return View(prescription);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching prescription details");
            return NotFound();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(int appointmentId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = GetUserRole();
        if (role != "Doctor" && role != "Admin") return Forbid();

        var existingPrescription = await _api.GetAsync<JsonElement>($"api/prescriptions/appointment/{appointmentId}", token);
        if (existingPrescription.ValueKind != JsonValueKind.Undefined && existingPrescription.ValueKind != JsonValueKind.Null)
        {
            return RedirectToAction(nameof(Details), new { appointmentId });
        }

        var appointment = await _api.GetAsync<JsonElement>($"api/appointments/{appointmentId}", token);
        if (appointment.ValueKind == JsonValueKind.Undefined || appointment.ValueKind == JsonValueKind.Null)
        {
            TempData["ErrorMessage"] = "Appointment not found";
            return RedirectToAction("Index", "Appointments");
        }

        ViewBag.Appointment = appointment;
        return View(new CreatePrescriptionViewModel { AppointmentId = appointmentId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int appointmentId, CreatePrescriptionViewModel model)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var role = GetUserRole();
        if (role != "Doctor" && role != "Admin") return Forbid();

        if (!ModelState.IsValid)
        {
            var appointment = await _api.GetAsync<JsonElement>($"api/appointments/{appointmentId}", token);
            ViewBag.Appointment = appointment;
            return View(model);
        }

        var result = await _api.PostAsync<JsonElement>("api/prescriptions", new
        {
            AppointmentId = appointmentId,
            model.Diagnosis,
            model.Medicines,
            model.Notes
        }, token);

        if (result.ValueKind == JsonValueKind.Undefined)
        {
            ModelState.AddModelError("", "Failed to save prescription.");
            var appointment = await _api.GetAsync<JsonElement>($"api/appointments/{appointmentId}", token);
            ViewBag.Appointment = appointment;
            return View(model);
        }

        TempData["Success"] = "Prescription saved successfully.";
        if (role == "Admin")
        {
            return RedirectToAction("Prescriptions", "Admin");
        }

        return RedirectToAction("Index", "Appointments");
    }

    [HttpGet]
    public async Task<IActionResult> Download(int appointmentId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");

        var prescription = await _api.GetAsync<JsonElement>($"api/prescriptions/appointment/{appointmentId}", token);
        if (prescription.ValueKind == JsonValueKind.Undefined || prescription.ValueKind == JsonValueKind.Null)
        {
            TempData["ErrorMessage"] = "Prescription not found.";
            return RedirectToAction(nameof(Index));
        }

        var content = new StringBuilder();
        content.AppendLine("SMART HOSPITAL - PRESCRIPTION");
        content.AppendLine($"Prescription ID: {prescription.GetProperty("prescriptionId").GetInt32()}");
        content.AppendLine($"Appointment ID: {prescription.GetProperty("appointmentId").GetInt32()}");
        content.AppendLine($"Patient Name: {GetString(prescription, "patientName")}");
        content.AppendLine($"Doctor Name: {GetString(prescription, "doctorName")}");
        content.AppendLine($"Appointment Date: {GetString(prescription, "appointmentDate")}");
        content.AppendLine();
        content.AppendLine($"Diagnosis: {GetString(prescription, "diagnosis")}");
        content.AppendLine($"Medicines: {GetString(prescription, "medicines")}");
        content.AppendLine($"Notes: {GetString(prescription, "notes")}");

        var bytes = Encoding.UTF8.GetBytes(content.ToString());
        var fileName = $"prescription-{prescription.GetProperty("prescriptionId").GetInt32()}.txt";
        return File(bytes, "text/plain", fileName);
    }

    private static string GetString(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var property) ? property.GetString() ?? string.Empty : string.Empty;
    }
}
