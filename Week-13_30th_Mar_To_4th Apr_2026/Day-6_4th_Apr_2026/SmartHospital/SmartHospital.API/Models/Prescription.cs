using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;

    [MaxLength(255)]
    public string? Diagnosis { get; set; }

    public string? Medicines { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}