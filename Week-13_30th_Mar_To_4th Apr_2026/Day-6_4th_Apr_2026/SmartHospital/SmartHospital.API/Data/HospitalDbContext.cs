using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Models;

namespace SmartHospital.API.Data;

public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<Bill> Bills => Set<Bill>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.UserId);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Role)
             .HasConversion<string>()
             .HasMaxLength(20);
        });

        // Department → Doctors (One-to-Many)
        modelBuilder.Entity<Doctor>(e =>
        {
            e.HasKey(d => d.DoctorId);
            e.HasIndex(d => d.UserId).IsUnique();

            e.HasOne(d => d.User)
             .WithOne(u => u.Doctor)
             .HasForeignKey<Doctor>(d => d.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(d => d.Department)
             .WithMany(dep => dep.Doctors)
             .HasForeignKey(d => d.DepartmentId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Patient → Appointments (One-to-Many)
        modelBuilder.Entity<Appointment>(e =>
        {
            e.HasKey(a => a.AppointmentId);

            e.HasOne(a => a.Patient)
             .WithMany(u => u.Appointments)
             .HasForeignKey(a => a.PatientId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(a => a.Doctor)
             .WithMany(d => d.Appointments)
             .HasForeignKey(a => a.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.Property(a => a.Status)
             .HasMaxLength(20)
             .HasDefaultValue("Booked");
        });

        // Appointment → Prescription (One-to-One)
        modelBuilder.Entity<Prescription>(e =>
        {
            e.HasKey(p => p.PrescriptionId);
            e.HasIndex(p => p.AppointmentId).IsUnique();

            e.HasOne(p => p.Appointment)
             .WithOne(a => a.Prescription)
             .HasForeignKey<Prescription>(p => p.AppointmentId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // Appointment → Bill (One-to-One)
        modelBuilder.Entity<Bill>(e =>
        {
            e.HasKey(b => b.BillId);

            e.HasOne(b => b.Appointment)
             .WithOne(a => a.Bill)
             .HasForeignKey<Bill>(b => b.AppointmentId)
             .OnDelete(DeleteBehavior.Cascade);

            e.Property(b => b.PaymentStatus)
             .HasMaxLength(20)
             .HasDefaultValue("Unpaid");
        });

        // Seed Data
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Cardiology", Description = "Heart & vascular diseases" },
            new Department { DepartmentId = 2, DepartmentName = "Neurology", Description = "Brain & nervous system" },
            new Department { DepartmentId = 3, DepartmentName = "Orthopedics", Description = "Bones & joints" },
            new Department { DepartmentId = 4, DepartmentName = "Pediatrics", Description = "Children's healthcare" },
            new Department { DepartmentId = 5, DepartmentName = "General Medicine", Description = "Primary care" }
        );
    }
}