using System.ComponentModel.DataAnnotations;

namespace LearnPlatform.API.DTOs;

public record EnrollDto([Required] int CourseId);

public record EnrollmentDto(
    int Id,
    int CourseId,
    string CourseTitle,
    DateTime EnrolledAt,
    string Status
);