using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.DTOs.Appointment;

public class CreateAppointmentDto
{
    [Required]
    public int PatientId { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }
}