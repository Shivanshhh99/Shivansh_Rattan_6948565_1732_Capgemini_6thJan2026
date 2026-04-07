using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required, MaxLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    // Navigation
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}