namespace SmartHospital.API.DTOs.Doctor;

public class DoctorDto
{
    public int DoctorId { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string? Availability { get; set; }
    public decimal ConsultationFee { get; set; }
}