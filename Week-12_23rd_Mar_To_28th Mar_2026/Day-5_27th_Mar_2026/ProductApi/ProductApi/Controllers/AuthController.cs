using Microsoft.AspNetCore.Mvc;
using ProductApi.DTOs;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request.Username != "admin" || request.Password != "1234")
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                });
            }

            var tokenResponse = _jwtService.GenerateToken(request.Username);

            return Ok(tokenResponse);
        }
    }
}