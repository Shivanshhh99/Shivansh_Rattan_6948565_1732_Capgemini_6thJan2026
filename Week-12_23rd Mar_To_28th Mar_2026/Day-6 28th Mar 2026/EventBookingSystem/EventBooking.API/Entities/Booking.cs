using System.ComponentModel.DataAnnotations;

namespace EventBooking.API.Entities;

public class Booking
{
    public int Id { get; set; }

    [Required]
    public int EventId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [Range(1, 50, ErrorMessage = "You can book between 1 and 50 seats.")]
    public int SeatsBooked { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
    public BookingStatus Status { get; set; } = BookingStatus.Confirmed;

    // Navigation
    public Event Event { get; set; } = null!;
}

public enum BookingStatus { Confirmed, Cancelled, Pending }