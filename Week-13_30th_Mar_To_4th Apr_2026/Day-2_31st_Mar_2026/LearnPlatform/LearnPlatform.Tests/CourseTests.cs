using AutoMapper;
using LearnPlatform.API.Controllers;
using LearnPlatform.API.Data;
using LearnPlatform.API.DTOs;
using LearnPlatform.API.Mappings;
using LearnPlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace LearnPlatform.Tests;

public class CourseTests
{
    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private IMapper CreateMapper()
    {
        var config = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
        return config.CreateMapper();
    }

    private CoursesController CreateController(AppDbContext db)
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        return new CoursesController(
            db,
            CreateMapper(),
            cache,
            NullLogger<CoursesController>.Instance
        );
    }

    // ── Test 1: Get all courses returns empty list initially ──────────
    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoCourses()
    {
        using var db = CreateInMemoryDb();
        var controller = CreateController(db);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var paged = Assert.IsType<PagedResult<CourseDto>>(ok.Value);
        Assert.Empty(paged.Items);
    }

    // ── Test 2: Get course by ID returns correct course ───────────────
    [Fact]
    public async Task GetById_ReturnsCourse_WhenExists()
    {
        using var db = CreateInMemoryDb();

        var instructor = new User
        {
            Username = "instructor1",
            Email = "i@test.com",
            PasswordHash = "hash",
            Role = "Instructor"
        };
        db.Users.Add(instructor);
        await db.SaveChangesAsync();

        var course = new Course
        {
            Title = "C# Basics",
            Category = "Programming",
            Price = 29.99m,
            InstructorId = instructor.Id
        };
        db.Courses.Add(course);
        await db.SaveChangesAsync();

        var controller = CreateController(db);
        var result = await controller.GetById(course.Id);

        var ok = Assert.IsType<OkObjectResult>(result);
        var dto = Assert.IsType<CourseDto>(ok.Value);
        Assert.Equal("C# Basics", dto.Title);
    }

    // ── Test 3: Get by invalid ID returns 404 ────────────────────────
    [Fact]
    public async Task GetById_Returns404_WhenNotFound()
    {
        using var db = CreateInMemoryDb();
        var controller = CreateController(db);

        var result = await controller.GetById(9999);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    // ── Test 4: Create course with invalid model returns 400 ──────────
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenModelInvalid()
    {
        using var db = CreateInMemoryDb();
        var controller = CreateController(db);
        controller.ModelState.AddModelError("Title", "Title is required");

        var dto = new CreateCourseDto
        {
            Title = "",
            Description = null,
            Category = "Programming",
            Price = 0
        };

        var result = await controller.Create(dto);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    // ── Test 5: GetByCategory returns filtered courses ────────────────
    [Fact]
    public async Task GetByCategory_ReturnsFilteredCourses()
    {
        using var db = CreateInMemoryDb();

        var instructor = new User
        {
            Username = "inst",
            Email = "inst@test.com",
            PasswordHash = "hash",
            Role = "Instructor"
        };
        db.Users.Add(instructor);
        await db.SaveChangesAsync();

        db.Courses.AddRange(
            new Course
            {
                Title = "Python 101",
                Category = "Programming",
                InstructorId = instructor.Id
            },
            new Course
            {
                Title = "Design Basics",
                Category = "Design",
                InstructorId = instructor.Id
            }
        );
        await db.SaveChangesAsync();

        var controller = CreateController(db);
        var result = await controller.GetByCategory("Programming");

        var ok = Assert.IsType<OkObjectResult>(result);
        var courses = Assert.IsAssignableFrom<IEnumerable<CourseDto>>(ok.Value);
        Assert.Single(courses);
        Assert.Equal("Python 101", courses.First().Title);
    }
}