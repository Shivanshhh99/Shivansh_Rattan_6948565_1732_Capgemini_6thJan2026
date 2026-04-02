using AutoMapper;
using LearnPlatform.API.Data;
using LearnPlatform.API.DTOs;
using LearnPlatform.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace LearnPlatform.API.Controllers;

[ApiController]
[Route("api/v1/courses")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CoursesController> _logger;
    private const string CacheKey = "all_courses";

    public CoursesController(
        AppDbContext db,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<CoursesController> logger)
    {
        _db = db;
        _mapper = mapper;
        _cache = cache;
        _logger = logger;
    }

    // GET /api/v1/courses?page=1&pageSize=10&search=python
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null)
    {
        // Only cache the unfiltered first page
        if (page == 1 && pageSize == 10 && string.IsNullOrEmpty(search))
        {
            if (_cache.TryGetValue(CacheKey, out PagedResult<CourseDto>? cached))
            {
                _logger.LogInformation("Returning cached courses");
                return Ok(cached);
            }
        }

        var query = _db.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(c =>
                c.Title.Contains(search) ||
                (c.Description != null && c.Description.Contains(search)));
        }

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var courses = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);

        var result = new PagedResult<CourseDto>
        {
            Items = courseDtos,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages
        };

        // Cache for 5 minutes (unfiltered first page only)
        if (page == 1 && pageSize == 10 && string.IsNullOrEmpty(search))
        {
            _cache.Set(CacheKey, result, TimeSpan.FromMinutes(5));
        }

        return Ok(result);
    }

    // GET /api/v1/courses/{id}
    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _db.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)
            return NotFound(new { error = $"Course {id} not found" });

        return Ok(_mapper.Map<CourseDto>(course));
    }

    // GET /api/v1/courses/category/{name}
    [HttpGet("category/{name}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCategory(string name)
    {
        var courses = await _db.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .Where(c => c.Category.ToLower() == name.ToLower())
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
    }

    // POST /api/v1/courses → Instructor only
    [HttpPost]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var course = _mapper.Map<Course>(dto);
        course.InstructorId = userId;

        _db.Courses.Add(course);
        await _db.SaveChangesAsync();

        // Invalidate cache when new course is added
        _cache.Remove(CacheKey);

        _logger.LogInformation("Course {Title} created by user {UserId}", dto.Title, userId);

        // Return the created course with full details
        var created = await _db.Courses
            .Include(c => c.Instructor)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
            .FirstAsync(c => c.Id == course.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = course.Id },
            _mapper.Map<CourseDto>(created));
    }

    // POST /api/v1/courses/{id}/lessons → Instructor only
    [HttpPost("{id:int}/lessons")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> AddLesson(int id, [FromBody] CreateLessonDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var course = await _db.Courses.FindAsync(id);
        if (course is null)
            return NotFound(new { error = $"Course {id} not found" });

        // Only the course owner or admin can add lessons
        if (course.InstructorId != userId && !User.IsInRole("Admin"))
            return Forbid();

        var lesson = _mapper.Map<Lesson>(dto);
        lesson.CourseId = id;

        _db.Lessons.Add(lesson);
        await _db.SaveChangesAsync();

        _cache.Remove(CacheKey);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            _mapper.Map<LessonDto>(lesson));
    }

    // DELETE /api/v1/courses/{id} → Admin only
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _db.Courses.FindAsync(id);
        if (course is null)
            return NotFound(new { error = $"Course {id} not found" });

        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
        _cache.Remove(CacheKey);

        return NoContent();
    }
}