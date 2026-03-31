using Microsoft.AspNetCore.Mvc;
using SecureJwtLoggingApi.DTOs;
using SecureJwtLoggingApi.Models;
using SecureJwtLoggingApi.Services;

namespace SecureJwtLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly JwtService _jwtService;

        // Dummy users
        private static readonly List<AppUser> _users = new()
        {
            new AppUser { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new AppUser { Id = 2, Username = "user1", Password = "user123", Role = "User" }
        };

        public AuthController(ILogger<AuthController> logger, JwtService jwtService)
        {
            _logger = logger;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login request received.");
                    return BadRequest(ModelState);
                }

                var user = _users.FirstOrDefault(u =>
                    u.Username == request.Username &&
                    u.Password == request.Password);

                if (user == null)
                {
                    _logger.LogWarning("Failed login attempt for username: {Username}", request.Username);
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                var token = _jwtService.GenerateToken(user);

                _logger.LogInformation("Token generated for user: {Username} | UserId: {UserId}", user.Username, user.Id);

                return Ok(new
                {
                    Message = "Login successful",
                    Token = token,
                    Username = user.Username,
                    Role = user.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during login for username: {Username}", request.Username);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}