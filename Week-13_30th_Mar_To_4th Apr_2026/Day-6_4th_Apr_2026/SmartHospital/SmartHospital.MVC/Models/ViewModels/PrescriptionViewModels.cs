using System.ComponentModel.DataAnnotations;

namespace SmartHospital.MVC.Models.ViewModels;

public class CreatePrescriptionViewModel
{
    public int AppointmentId { get; set; }

    [MaxLength(255)]
    public string? Diagnosis { get; set; }

    public string? Medicines { get; set; }

    [MaxLength(255)]
    public string? Notes { get; set; }
}

public class AdminPrescriptionViewModel
{
    public int PrescriptionId { get; set; }
    public int AppointmentId { get; set; }
    public string? Diagnosis { get; set; }
    public string? Medicines { get; set; }
    public string? Notes { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
}