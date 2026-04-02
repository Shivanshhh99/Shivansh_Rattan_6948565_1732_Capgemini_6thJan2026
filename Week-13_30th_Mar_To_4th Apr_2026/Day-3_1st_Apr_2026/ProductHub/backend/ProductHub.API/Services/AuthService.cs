using ProductHub.API.DTOs;
using ProductHub.API.Helpers;
using ProductHub.API.Models;
using ProductHub.API.Repositories;

namespace ProductHub.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IUserRepository userRepository, JwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _jwtHelper = jwtHelper;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);

        if (existingUser != null)
            return "Username already exists";

        var user = new User
        {
            Username = dto.Username,
            Password = dto.Password
        };

        await _userRepository.AddUserAsync(user);
        return "User registered successfully";
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);

        if (user == null || user.Password != dto.Password)
            return null;

        return _jwtHelper.GenerateToken(user);
    }
}