using System.ComponentModel.DataAnnotations;

namespace EventBooking.API.DTOs;

public class BookingDto
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string EventTitle { get; set; } = string.Empty;
    public string EventLocation { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int SeatsBooked { get; set; }
    public DateTime BookedAt { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CreateBookingDto
{
    [Required]
    public int EventId { get; set; }

    [Required]
    [Range(1, 50, ErrorMessage = "Seats must be between 1 and 50.")]
    public int SeatsBooked { get; set; }
}