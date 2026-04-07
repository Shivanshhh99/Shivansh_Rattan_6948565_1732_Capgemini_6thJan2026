using Microsoft.AspNetCore.Mvc;
using SmartHealthcare.MVC.Services;
using SmartHealthcare.Models.DTOs;

namespace SmartHealthcare.MVC.Controllers;

public class AppointmentController : Controller
{
    private readonly IApiService _apiService;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(IApiService apiService, ILogger<AppointmentController> logger)
    {
        _apiService = apiService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var role = HttpContext.Session.GetString("Role");
        PagedResult<AppointmentDTO>? result = null;

        if (role == "Patient")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments/my-appointments?pageNumber=1&pageSize=50", token);
        }
        else if (role == "Doctor")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments/doctor-appointments?pageNumber=1&pageSize=50", token);
        }
        else if (role == "Admin")
        {
            result = await _apiService.GetAsync<PagedResult<AppointmentDTO>>("appointments?pageNumber=1&pageSize=50", token);
        }

        ViewBag.Role = role;
        return View(result?.Items ?? Enumerable.Empty<AppointmentDTO>());
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        // Try to fetch doctors - handle both paginated and direct responses
        IEnumerable<DoctorDTO>? doctors = null;
        
        // First try with pagination (in case API returns PagedResult)
        var pagedResult = await _apiService.GetAsync<PagedResult<DoctorDTO>>("doctors?pageNumber=1&pageSize=100", token);
        if (pagedResult?.Items != null)
        {
            doctors = pagedResult.Items;
        }
        else
        {
            // Try without pagination (in case API returns direct list)
            doctors = await _apiService.GetAsync<IEnumerable<DoctorDTO>>("doctors", token);
        }

        ViewBag.Doctors = doctors ?? Enumerable.Empty<DoctorDTO>();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDTO dto)
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            var pagedResult = await _apiService.GetAsync<PagedResult<DoctorDTO>>("doctors?pageNumber=1&pageSize=100", token);
            var doctors = pagedResult?.Items ?? await _apiService.GetAsync<IEnumerable<DoctorDTO>>("doctors", token);
            ViewBag.Doctors = doctors ?? Enumerable.Empty<DoctorDTO>();
            return View(dto);
        }

        var result = await _apiService.PostAsync<AppointmentDTO>("appointments", dto, token);
        if (result == null)
        {
            ModelState.AddModelError(string.Empty, "Failed to create appointment. Please ensure your patient profile is set up.");
            var pagedResult = await _apiService.GetAsync<PagedResult<DoctorDTO>>("doctors?pageNumber=1&pageSize=100", token);
            var doctors = pagedResult?.Items ?? await _apiService.GetAsync<IEnumerable<DoctorDTO>>("doctors", token);
            ViewBag.Doctors = doctors ?? Enumerable.Empty<DoctorDTO>();
            return View(dto);
        }

        _logger.LogInformation("Appointment booked successfully");
        TempData["Success"] = "Appointment booked successfully!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var token = HttpContext.Session.GetString("Token");
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Login", "Account");
        }

        var appointment = await _apiService.GetAsync<AppointmentDTO>($"appointments/{id}", token);
        if (appointment == null)
        {
            return NotFound();
        }

        ViewBag.Role = HttpContext.Session.GetString("Role");
        ViewBag.Prescription = await _apiService.GetAsync<PrescriptionDTO>($"prescriptions/appointment/{id}", token);
        return View(appointment);
    }
}