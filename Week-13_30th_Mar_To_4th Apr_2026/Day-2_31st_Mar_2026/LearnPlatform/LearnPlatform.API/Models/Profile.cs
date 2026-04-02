namespace LearnPlatform.API.Models;

public class Profile
{
    public int Id { get; set; }
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Website { get; set; }

    // FK for One-to-One
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}