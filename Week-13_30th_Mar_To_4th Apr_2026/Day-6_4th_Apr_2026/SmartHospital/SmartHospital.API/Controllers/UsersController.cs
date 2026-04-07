using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs.Auth;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUnitOfWork uow, ILogger<UsersController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _uow.Users.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all users");
            return StatusCode(500, new { message = "Error retrieving users" });
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        try
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user {UserId}", userId);
            return StatusCode(500, new { message = "Error retrieving user" });
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            var user = await _uow.Users.GetByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Prevent deleting admin users
            if (user.Role == "Admin")
                return BadRequest(new { message = "Cannot delete admin users" });

            await _uow.Users.DeleteAsync(user);
            await _uow.SaveChangesAsync();

            _logger.LogInformation("User {UserId} deleted successfully", userId);
            return Ok(new { message = "User deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {UserId}", userId);
            return StatusCode(500, new { message = "Error deleting user" });
        }
    }
}
