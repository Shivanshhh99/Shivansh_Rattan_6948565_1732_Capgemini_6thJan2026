using AutoMapper;
using LearnPlatform.API.Data;
using LearnPlatform.API.DTOs;
using LearnPlatform.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnPlatform.API.Controllers;

[ApiController]
[Route("api/v1/enroll")]
[Authorize]
public class EnrollController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EnrollController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // POST /api/v1/enroll  → Student or Admin
    [HttpPost]
    [Authorize(Roles = "Student,Admin")]
    public async Task<IActionResult> Enroll([FromBody] EnrollDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var courseExists = await _db.Courses.AnyAsync(c => c.Id == dto.CourseId);
        if (!courseExists)
            return NotFound(new { error = "Course not found" });

        var alreadyEnrolled = await _db.Enrollments
            .AnyAsync(e => e.UserId == userId && e.CourseId == dto.CourseId);

        if (alreadyEnrolled)
            return Conflict(new { error = "Already enrolled in this course" });

        var enrollment = new Enrollment
        {
            UserId = userId,
            CourseId = dto.CourseId
        };

        _db.Enrollments.Add(enrollment);
        await _db.SaveChangesAsync();

        // Return with full details
        var created = await _db.Enrollments
            .Include(e => e.Course)
            .FirstAsync(e => e.Id == enrollment.Id);

        return Ok(_mapper.Map<EnrollmentDto>(created));
    }

    // GET /api/v1/enroll/my  → Get current user's enrollments
    [HttpGet("my")]
    public async Task<IActionResult> MyEnrollments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var enrollments = await _db.Enrollments
            .Include(e => e.Course)
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.EnrolledAt)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollments));
    }
}