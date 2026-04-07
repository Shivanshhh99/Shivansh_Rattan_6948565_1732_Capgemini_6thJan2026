namespace ECommerce.API.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // demo only
    public string Role { get; set; } = "User";

    public UserProfile? Profile { get; set; }
    public List<Order> Orders { get; set; } = new();
}