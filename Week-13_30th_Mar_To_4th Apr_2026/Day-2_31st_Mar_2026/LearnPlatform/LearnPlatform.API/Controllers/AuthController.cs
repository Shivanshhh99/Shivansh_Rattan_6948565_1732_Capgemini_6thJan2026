using LearnPlatform.API.Data;
using LearnPlatform.API.DTOs;
using LearnPlatform.API.Models;
using LearnPlatform.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LearnPlatform.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IJwtService _jwt;
    private readonly ILogger<AuthController> _logger;

    public AuthController(AppDbContext db, IJwtService jwt, ILogger<AuthController> logger)
    {
        _db = db;
        _jwt = jwt;
        _logger = logger;
    }

    // POST /api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            return BadRequest(new { error = "Email already registered" });

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = HashPassword(dto.Password),
            Role = dto.Role,
            Profile = new Models.Profile()  // Auto-create profile (One-to-One)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        _logger.LogInformation("New user registered: {Email} with role {Role}", dto.Email, dto.Role);

        var token = _jwt.GenerateToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();

        return Ok(new AuthResponseDto(
            token, refreshToken, user.Username, user.Role,
            DateTime.UtcNow.AddHours(2)
        ));
    }

    // POST /api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (user is null || !VerifyPassword(dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Failed login attempt for {Email}", dto.Email);
            return Unauthorized(new { error = "Invalid credentials" });
        }

        var token = _jwt.GenerateToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();

        _logger.LogInformation("User {Email} logged in successfully", dto.Email);

        return Ok(new AuthResponseDto(
            token, refreshToken, user.Username, user.Role,
            DateTime.UtcNow.AddHours(2)
        ));
    }

    // POST /api/auth/refresh
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        // Simplified — in production validate stored refresh token
        if (!_jwt.ValidateRefreshToken(refreshToken))
            return Unauthorized(new { error = "Invalid refresh token" });

        return Ok(new { message = "Refresh token accepted (extend with DB storage)" });
    }

    private static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "LearnPlatformSalt"));
        return Convert.ToBase64String(bytes);
    }

    private static bool VerifyPassword(string password, string hash)
        => HashPassword(password) == hash;
}