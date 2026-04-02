using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.Models;

public class Course
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required, MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // FK
    public int InstructorId { get; set; }
    public User Instructor { get; set; } = null!;

    // One-to-Many: Course → Lessons
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    // Many-to-Many: Course ↔ User via Enrollment
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}