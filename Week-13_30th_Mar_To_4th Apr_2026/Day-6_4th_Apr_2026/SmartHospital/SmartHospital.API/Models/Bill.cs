using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHospital.API.Models;

public class Bill
{
    public int BillId { get; set; }

    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ConsultationFee { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MedicineCharges { get; set; }

    [NotMapped]
    public decimal TotalAmount => ConsultationFee + MedicineCharges;

    [Required]
    public string PaymentStatus { get; set; } = "Unpaid"; // Paid | Unpaid
}