using LearnPlatform.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnPlatform.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Models.Profile> Profiles => Set<Models.Profile>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── One-to-One: User ↔ Profile ──────────────────────────────
        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Models.Profile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── One-to-Many: User → Courses (as Instructor) ─────────────
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(u => u.CreatedCourses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ── One-to-Many: Course → Lessons ────────────────────────────
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // ── Many-to-Many: User ↔ Course via Enrollment ───────────────
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique constraint: one enrollment per user per course
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.UserId, e.CourseId })
            .IsUnique();

        // ── Column precision ─────────────────────────────────────────
        modelBuilder.Entity<Course>()
            .Property(c => c.Price)
            .HasPrecision(18, 2);

        // ── Indexes for performance ───────────────────────────────────
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Category);

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Title);
    }
}