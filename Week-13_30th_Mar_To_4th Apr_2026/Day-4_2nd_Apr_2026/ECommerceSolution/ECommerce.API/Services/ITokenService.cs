namespace ECommerce.API.Services;

public interface ITokenService
{
    string GenerateToken(string username, string role);
    string GenerateRefreshToken();
}