using System.ComponentModel.DataAnnotations;

namespace SmartHospital.MVC.Models.ViewModels;

public class BookAppointmentViewModel
{
    [Required(ErrorMessage = "Please select a doctor")]
    [Display(Name = "Doctor")]
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Please select an appointment date")]
    [Display(Name = "Appointment Date & Time")]
    [DataType(DataType.DateTime)]
    [FutureDate(ErrorMessage = "Appointment date must be in the future")]
    public DateTime AppointmentDate { get; set; }

    public int PatientId { get; set; }
    public int? FilterDepartmentId { get; set; }
}

// Custom validation attribute
public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is DateTime date)
            return date > DateTime.Now;
        return false;
    }
}