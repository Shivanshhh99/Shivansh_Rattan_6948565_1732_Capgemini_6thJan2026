using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs.Auth;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Helpers;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly JwtHelper _jwtHelper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUnitOfWork uow, JwtHelper jwtHelper, ILogger<AuthController> logger)
    {
        _uow = uow;
        _jwtHelper = jwtHelper;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingUser = await _uow.Users.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new DuplicateEmailException(dto.Email);

        var allowedRoles = new[] { "Patient", "Admin" };
        if (!allowedRoles.Contains(dto.Role))
            return BadRequest(new { message = "Invalid role specified" });

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("New user registered: {Email}, Role: {Role}", user.Email, user.Role);

        var token = _jwtHelper.GenerateToken(user);

        return Ok(new AuthResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            UserId = user.UserId
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _uow.Users.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid email or password" });

        _logger.LogInformation("User logged in: {Email}", user.Email);

        var token = _jwtHelper.GenerateToken(user);

        return Ok(new AuthResponseDto
        {
            Token = token,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            UserId = user.UserId
        });
    }
}