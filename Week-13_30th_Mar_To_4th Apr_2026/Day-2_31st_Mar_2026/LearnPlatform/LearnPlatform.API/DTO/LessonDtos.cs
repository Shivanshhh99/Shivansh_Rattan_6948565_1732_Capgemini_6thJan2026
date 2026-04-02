using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.DTOs;

public record LessonDto(
    int Id,
    string Title,
    string? Content,
    int Order,
    int DurationMinutes,
    int CourseId
);

public record CreateLessonDto(
    [Required, MaxLength(200)] string Title,
    string? Content,
    [Range(1, 1000)] int Order,
    [Range(1, 600)] int DurationMinutes
);