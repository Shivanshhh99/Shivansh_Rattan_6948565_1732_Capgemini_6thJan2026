using ProductApi.DTOs;

namespace ProductApi.Services
{
    public interface IJwtService
    {
        LoginResponseDto GenerateToken(string username);
    }
}