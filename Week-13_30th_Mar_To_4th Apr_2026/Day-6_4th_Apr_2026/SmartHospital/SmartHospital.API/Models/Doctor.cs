using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHospital.API.Models;

public class Doctor
{
    public int DoctorId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    [MaxLength(100)]
    public string? Specialization { get; set; }

    public int ExperienceYears { get; set; }

    [MaxLength(100)]
    public string? Availability { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ConsultationFee { get; set; }

    // Navigation
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}