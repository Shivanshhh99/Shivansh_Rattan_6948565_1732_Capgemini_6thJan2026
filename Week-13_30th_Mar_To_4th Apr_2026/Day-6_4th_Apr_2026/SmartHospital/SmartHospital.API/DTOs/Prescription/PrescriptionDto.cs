namespace SmartHospital.API.DTOs.Prescription;

public class PrescriptionDto
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