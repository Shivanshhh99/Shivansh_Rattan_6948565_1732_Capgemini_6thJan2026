using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.Models;

public class Lesson
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }
    public int Order { get; set; }
    public int DurationMinutes { get; set; }

    // FK
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}