using ECommerce.API.DTOs;
using ECommerce.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        if (dto.Username == "admin" && dto.Password == "123")
        {
            var token = _tokenService.GenerateToken(dto.Username, "Admin");
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken,
                Role = "Admin"
            });
        }

        if (dto.Username == "user" && dto.Password == "123")
        {
            var token = _tokenService.GenerateToken(dto.Username, "User");
            var refreshToken = _tokenService.GenerateRefreshToken();

            return Ok(new
            {
                Token = token,
                RefreshToken = refreshToken,
                Role = "User"
            });
        }

        return Unauthorized("Invalid credentials");
    }

    [HttpPost("refresh")]
    public IActionResult Refresh(TokenRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            return BadRequest("Invalid refresh token");

        var newToken = _tokenService.GenerateToken("user", "User");
        return Ok(new { Token = newToken });
    }
}