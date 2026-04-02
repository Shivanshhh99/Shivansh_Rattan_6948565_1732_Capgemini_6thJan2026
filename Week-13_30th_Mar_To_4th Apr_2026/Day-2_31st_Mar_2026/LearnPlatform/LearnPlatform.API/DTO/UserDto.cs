namespace LearnPlatform.API.DTOs;

public record UserDto(
    int Id,
    string Username,
    string Email,
    string Role,
    string? Bio,
    DateTime CreatedAt
);