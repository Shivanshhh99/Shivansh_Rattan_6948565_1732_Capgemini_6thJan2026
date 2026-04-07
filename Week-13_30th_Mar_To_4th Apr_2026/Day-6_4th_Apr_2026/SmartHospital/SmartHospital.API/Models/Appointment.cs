using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.Models;

public class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }
    public User Patient { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;

    [Required]
    public DateTime AppointmentDate { get; set; }

    [Required]
    public string Status { get; set; } = "Booked"; // Booked | Completed | Cancelled

    // Navigation
    public Prescription? Prescription { get; set; }
    public Bill? Bill { get; set; }
}