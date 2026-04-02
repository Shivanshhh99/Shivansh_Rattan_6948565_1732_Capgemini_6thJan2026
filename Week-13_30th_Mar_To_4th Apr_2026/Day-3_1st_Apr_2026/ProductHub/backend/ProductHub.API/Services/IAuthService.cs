using ProductHub.API.DTOs;

namespace ProductHub.API.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDto dto);
    Task<string> RegisterAsync(RegisterDto dto);
}