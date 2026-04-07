using Microsoft.AspNetCore.Mvc;
using SmartHospital.MVC.Helpers;
using SmartHospital.MVC.Models.ViewModels;
using SmartHospital.MVC.Services.Interfaces;

namespace SmartHospital.MVC.Controllers;

public class BillsController : Controller
{
    private readonly IApiService _api;

    public BillsController(IApiService api)
    {
        _api = api;
    }

    private string? GetToken() => HttpContext.Session.GetString(SessionHelper.JwtToken);

    private bool IsPatient() => HttpContext.Session.GetString(SessionHelper.UserRole) == "Patient";

    public async Task<IActionResult> Index()
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");
        if (!IsPatient()) return Forbid();

        var userId = HttpContext.Session.GetInt32(SessionHelper.UserId);
        if (!userId.HasValue) return RedirectToAction("Login", "Auth");

        var bills = await _api.GetAsync<List<AdminBillViewModel>>($"api/billing/patient/{userId.Value}", token);
        return View(bills ?? new List<AdminBillViewModel>());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(int billId)
    {
        var token = GetToken();
        if (token == null) return RedirectToAction("Login", "Auth");
        if (!IsPatient()) return Forbid();

        var success = await _api.PutAsync($"api/billing/{billId}/pay", new { }, token);

        if (success)
            TempData["Success"] = "Payment successful. Bill marked as paid.";
        else
            TempData["Error"] = "Unable to pay bill right now. Please try again.";

        return RedirectToAction(nameof(Index));
    }
}
