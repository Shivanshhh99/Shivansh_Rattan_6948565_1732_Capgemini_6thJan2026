using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.DTOs;

public record RegisterDto(
    [Required, MaxLength(100)] string Username,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    string Role = "Student"  // Default to Student
);

public record LoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password
);

public record AuthResponseDto(
    string Token,
    string RefreshToken,
    string Username,
    string Role,
    DateTime ExpiresAt
);