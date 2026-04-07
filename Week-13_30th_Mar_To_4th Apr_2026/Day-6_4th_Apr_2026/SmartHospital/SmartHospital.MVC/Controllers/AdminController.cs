using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;
using System.Text.Json;

namespace SmartHospital.MVC.Controllers;

public class AdminController : Controller
{
    private readonly IApiService _api;
    private readonly ILogger<AdminController> _logger;

    public AdminController(IApiService api, ILogger<AdminController> logger)
    {
        _api = api;
        _logger = logger;
    }

    private IActionResult CheckAdminRole()
    {
        var role = HttpContext.Session.GetString(SessionHelper.UserRole);
        var token = HttpContext.Session.GetString(SessionHelper.JwtToken);

        if (token == null || role != "Admin")
            return RedirectToAction("Login", "Auth");

        return null!;
    }

    [HttpGet]
    public async Task<IActionResult> Dashboard()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;

        ViewBag.UserName = HttpContext.Session.GetString(SessionHelper.UserName);

        try
        {
            var users = await _api.GetAsync<List<dynamic>>("api/users", token);
            var doctors = await _api.GetAsync<List<dynamic>>("api/doctors", token);
            var appointments = await _api.GetAsync<List<dynamic>>("api/appointments", token);
            var departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
            var bills = await _api.GetAsync<List<dynamic>>("api/billing", token);
            var prescriptions = await _api.GetAsync<List<dynamic>>("api/prescriptions", token);

            ViewBag.TotalUsers = users?.Count ?? 0;
            ViewBag.TotalDoctors = doctors?.Count ?? 0;
            ViewBag.TotalAppointments = appointments?.Count ?? 0;
            ViewBag.TotalDepartments = departments?.Count ?? 0;
            ViewBag.TotalBills = bills?.Count ?? 0;
            ViewBag.TotalPrescriptions = prescriptions?.Count ?? 0;

            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in admin dashboard");
            ModelState.AddModelError("", "Error loading dashboard data");
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var users = await _api.GetAsync<List<AdminUserViewModel>>("api/users", token);

        return View(users ?? new List<AdminUserViewModel>());
    }

    [HttpGet]
    public async Task<IActionResult> Doctors()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var doctors = await _api.GetAsync<List<AdminDoctorViewModel>>("api/doctors", token);

        ViewBag.Departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
        return View(doctors ?? new List<AdminDoctorViewModel>());
    }

    [HttpGet("create-doctor")]
    public async Task<IActionResult> CreateDoctor()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
        ViewBag.Departments = departments;
        
        return View(new CreateDoctorViewModel());
    }

    [HttpPost("create-doctor")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDoctor(CreateDoctorViewModel model)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        if (!ModelState.IsValid)
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
            return View(model);
        }

        try
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            var success = await _api.PostAsync<dynamic>("api/doctors", new
            {
                model.FullName,
                model.Email,
                model.Password,
                model.DepartmentId,
                model.Specialization,
                model.ExperienceYears,
                model.Availability,
                model.ConsultationFee
            }, token);

            TempData["SuccessMessage"] = "Doctor added successfully!";
            return RedirectToAction("Doctors");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating doctor");
            ModelState.AddModelError("", "Failed to create doctor: " + ex.Message);
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
            return View(model);
        }
    }

    [HttpGet("edit-doctor/{id}")]
    public async Task<IActionResult> EditDoctor(int id)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var doctor = await _api.GetAsync<AdminDoctorViewModel>($"api/doctors/{id}", token);
        var departments = await _api.GetAsync<List<dynamic>>("api/departments", token);

        if (doctor == null)
            return NotFound();

        ViewBag.Departments = departments;
        return View(new UpdateDoctorViewModel
        {
            DoctorId = doctor.DoctorId,
            FullName = doctor.FullName,
            DepartmentId = doctor.DepartmentId,
            Specialization = doctor.Specialization,
            ExperienceYears = doctor.ExperienceYears,
            Availability = doctor.Availability
            ,
            ConsultationFee = doctor.ConsultationFee
        });
    }

    [HttpPost("edit-doctor/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDoctor(int id, UpdateDoctorViewModel model)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        if (!ModelState.IsValid)
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
            return View(model);
        }

        try
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            var success = await _api.PutAsync($"api/doctors/{id}", new
            {
                model.FullName,
                model.DepartmentId,
                model.Specialization,
                model.ExperienceYears,
                model.Availability,
                model.ConsultationFee
            }, token);

            TempData["SuccessMessage"] = "Doctor updated successfully!";
            return RedirectToAction("Doctors");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating doctor");
            ModelState.AddModelError("", "Failed to update doctor: " + ex.Message);
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Departments = await _api.GetAsync<List<dynamic>>("api/departments", token);
            return View(model);
        }
    }

    [HttpPost("delete-doctor/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var success = await _api.DeleteAsync($"api/doctors/{id}", token);

        if (success)
            TempData["SuccessMessage"] = "Doctor deleted successfully";
        else
            TempData["ErrorMessage"] = "Failed to delete doctor";

        return RedirectToAction("Doctors");
    }

    [HttpGet]
    public async Task<IActionResult> Appointments()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var appointments = await _api.GetAsync<List<AdminAppointmentViewModel>>("api/appointments", token);

        return View(appointments ?? new List<AdminAppointmentViewModel>());
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAppointmentStatus(int appointmentId, [FromForm] string newStatus)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var success = await _api.PutAsync($"api/appointments/{appointmentId}/status", newStatus, token);

        if (success)
            TempData["SuccessMessage"] = "Appointment status updated successfully";
        else
            TempData["ErrorMessage"] = "Failed to update appointment status";

        return RedirectToAction("Appointments");
    }

    [HttpGet]
    public async Task<IActionResult> Billing()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var bills = await _api.GetAsync<List<AdminBillViewModel>>("api/billing", token);

        return View(bills ?? new List<AdminBillViewModel>());
    }

    [HttpGet("create-bill")]
    public async Task<IActionResult> CreateBill()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var appointments = await _api.GetAsync<List<AdminAppointmentViewModel>>("api/appointments", token);
        ViewBag.Appointments = appointments;

        return View(new CreateBillViewModel());
    }

    [HttpPost("create-bill")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBill(CreateBillViewModel model)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        if (!ModelState.IsValid)
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Appointments = await _api.GetAsync<List<AdminAppointmentViewModel>>("api/appointments", token);
            return View(model);
        }

        try
        {
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            var result = await _api.PostAsync<AdminBillViewModel>("api/billing", new
            {
                model.AppointmentId,
                model.ConsultationFee,
                model.MedicineCharges
            }, token);

            if (result == null)
            {
                ModelState.AddModelError("", "Failed to create/update bill.");
                ViewBag.Appointments = await _api.GetAsync<List<AdminAppointmentViewModel>>("api/appointments", token);
                return View(model);
            }

            TempData["SuccessMessage"] = "Bill created successfully!";
            return RedirectToAction("Billing");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bill");
            ModelState.AddModelError("", "Failed to create bill: " + ex.Message);
            var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
            ViewBag.Appointments = await _api.GetAsync<List<dynamic>>("api/appointments", token);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Departments()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var departments = await _api.GetAsync<List<AdminDepartmentViewModel>>("api/departments", token);

        return View(departments ?? new List<AdminDepartmentViewModel>());
    }

    [HttpGet]
    public async Task<IActionResult> Prescriptions()
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var prescriptions = await _api.GetAsync<List<AdminPrescriptionViewModel>>("api/prescriptions", token);

        return View(prescriptions ?? new List<AdminPrescriptionViewModel>());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var check = CheckAdminRole();
        if (check != null) return check;

        var token = HttpContext.Session.GetString(SessionHelper.JwtToken)!;
        var success = await _api.DeleteAsync($"api/users/{userId}", token);

        if (success)
            TempData["SuccessMessage"] = "User deleted successfully";
        else
            TempData["ErrorMessage"] = "Failed to delete user";

        return RedirectToAction("Users");
    }
}
