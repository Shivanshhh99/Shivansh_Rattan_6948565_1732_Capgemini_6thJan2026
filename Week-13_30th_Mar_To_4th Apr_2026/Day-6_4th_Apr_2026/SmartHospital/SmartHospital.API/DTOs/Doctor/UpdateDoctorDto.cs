using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.DTOs.Doctor;

public class UpdateDoctorDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required]
    public int DepartmentId { get; set; }

    [MaxLength(100)]
    public string? Specialization { get; set; }

    [Range(0, 50)]
    public int ExperienceYears { get; set; }

    [MaxLength(100)]
    public string? Availability { get; set; }

    [Range(0, 100000)]
    public decimal ConsultationFee { get; set; }
}