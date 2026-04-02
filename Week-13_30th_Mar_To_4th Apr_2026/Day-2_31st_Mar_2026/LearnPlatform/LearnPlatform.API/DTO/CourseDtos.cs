using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string InstructorName { get; set; } = string.Empty;
    public int LessonCount { get; set; }
    public int EnrollmentCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateCourseDto
{
    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required, MaxLength(100)]
    public string Category { get; set; } = string.Empty;

    [Range(0, 9999.99)]
    public decimal Price { get; set; }
}

public class UpdateCourseDto
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Category { get; set; }

    [Range(0, 9999.99)]
    public decimal? Price { get; set; }
}

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}