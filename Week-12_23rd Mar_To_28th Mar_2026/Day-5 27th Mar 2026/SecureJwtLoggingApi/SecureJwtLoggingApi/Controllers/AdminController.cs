using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SecureJwtLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpGet("dashboard")]
        public IActionResult GetDashboard()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;

            _logger.LogInformation("GET /api/admin/dashboard called by Admin UserId: {UserId}, Username: {Username}", userId, username);

            return Ok(new
            {
                Message = "Welcome to Admin Dashboard",
                TotalUsers = 250,
                TotalOrders = 540
            });
        }
    }
}