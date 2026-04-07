using System.ComponentModel.DataAnnotations;

namespace SmartHospital.MVC.Models.ViewModels;

public class CreateDoctorViewModel
{
    [Required(ErrorMessage = "Full name is required")]
    [MaxLength(100)]
    [Display(Name = "Doctor Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a department")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    [MaxLength(100)]
    [Display(Name = "Specialization")]
    public string? Specialization { get; set; }

    [Range(0, 50, ErrorMessage = "Experience must be between 0 and 50 years")]
    [Display(Name = "Years of Experience")]
    public int ExperienceYears { get; set; }

    [MaxLength(100)]
    [Display(Name = "Availability Schedule")]
    public string? Availability { get; set; }

    [Range(0, 100000, ErrorMessage = "Consultation fee must be between 0 and 100000")]
    [Display(Name = "Consultation Fee")]
    [DataType(DataType.Currency)]
    public decimal ConsultationFee { get; set; }
}

public class UpdateDoctorViewModel
{
    public int DoctorId { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [MaxLength(100)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Department is required")]
    [Display(Name = "Department")]
    public int DepartmentId { get; set; }

    [MaxLength(100)]
    [Display(Name = "Specialization")]
    public string? Specialization { get; set; }

    [Required(ErrorMessage = "Experience years is required")]
    [Range(0, 60)]
    [Display(Name = "Experience (Years)")]
    public int ExperienceYears { get; set; }

    [MaxLength(100)]
    [Display(Name = "Availability")]
    public string? Availability { get; set; }

    [Range(0, 100000, ErrorMessage = "Consultation fee must be between 0 and 100000")]
    [Display(Name = "Consultation Fee")]
    [DataType(DataType.Currency)]
    public decimal ConsultationFee { get; set; }
}

public class CreateBillViewModel
{
    [Required(ErrorMessage = "Appointment is required")]
    [Display(Name = "Appointment")]
    public int AppointmentId { get; set; }

    [Required(ErrorMessage = "Consultation fee is required")]
    [Range(0, 999999)]
    [Display(Name = "Consultation Fee")]
    [DataType(DataType.Currency)]
    public decimal ConsultationFee { get; set; }

    [Required(ErrorMessage = "Medicine charges are required")]
    [Range(0, 999999)]
    [Display(Name = "Medicine Charges")]
    [DataType(DataType.Currency)]
    public decimal MedicineCharges { get; set; }
}