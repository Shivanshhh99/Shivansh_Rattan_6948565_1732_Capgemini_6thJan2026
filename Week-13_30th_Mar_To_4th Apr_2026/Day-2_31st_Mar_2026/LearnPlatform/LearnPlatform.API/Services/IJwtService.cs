using LearnPlatform.API.Models;

namespace LearnPlatform.API.Services;

public interface IJwtService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string token);
}