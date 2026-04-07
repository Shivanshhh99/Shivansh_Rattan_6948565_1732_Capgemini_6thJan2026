using System.ComponentModel.DataAnnotations;

namespace SmartHospital.API.DTOs.Billing;

public class CreateBillDto
{
    [Required]
    public int AppointmentId { get; set; }

    [Range(0, 100000, ErrorMessage = "Consultation fee must be between 0 and 100000")]
    public decimal ConsultationFee { get; set; }

    [Range(0, 100000, ErrorMessage = "Medicine charges must be between 0 and 100000")]
    public decimal MedicineCharges { get; set; }
}