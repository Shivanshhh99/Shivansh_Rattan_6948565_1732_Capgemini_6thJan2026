namespace LearnPlatform.API.Models;

// Join entity for Many-to-Many: User ↔ Course
public class Enrollment
{
    public int Id { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Active"; // Active | Completed | Cancelled

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}