using ECommerceLoggingApi.DTOs;
using ECommerceLoggingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceLoggingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            try
            {
                _logger.LogInformation("Login attempt: {Email}", request.Email);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid login input received for email: {Email}", request.Email);
                    return BadRequest(ModelState);
                }

                var user = _userService.ValidateUser(request.Email, request.Password);

                if (user == null)
                {
                    if (_userService.UserExists(request.Email))
                    {
                        _logger.LogWarning("Invalid password for email: {Email}", request.Email);
                        return Unauthorized("Invalid password");
                    }

                    _logger.LogWarning("Invalid login attempt. User not found: {Email}", request.Email);
                    return Unauthorized("User not found");
                }

                _logger.LogInformation("User login successful: {Email}", request.Email);

                return Ok(new
                {
                    Message = "Login successful",
                    UserId = user.Id,
                    user.Email
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred during login for email: {Email}", request.Email);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}