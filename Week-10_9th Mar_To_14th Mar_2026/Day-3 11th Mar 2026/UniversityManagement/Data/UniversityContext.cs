using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Department → Instructor (One to Many)
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Instructors)
                .WithOne(i => i.Department)
                .HasForeignKey(i => i.DepartmentId);

            // Instructor → Course (One to Many)
            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId);

            // Student → Enrollment (One to Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            // Course → Enrollment (One to Many)
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
    }
}