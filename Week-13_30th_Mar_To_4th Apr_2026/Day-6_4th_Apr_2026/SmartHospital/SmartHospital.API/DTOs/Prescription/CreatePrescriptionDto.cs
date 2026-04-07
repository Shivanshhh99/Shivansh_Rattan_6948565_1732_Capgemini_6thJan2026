using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.DTOs.Prescription;

public class CreatePrescriptionDto
{
    [Required]
    public int AppointmentId { get; set; }

    [MaxLength(255)]
    public string? Diagnosis { get; set; }

    public string? Medicines { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}