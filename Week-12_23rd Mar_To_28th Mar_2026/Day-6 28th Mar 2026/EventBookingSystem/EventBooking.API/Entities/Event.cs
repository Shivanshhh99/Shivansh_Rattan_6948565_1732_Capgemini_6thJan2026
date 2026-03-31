using System.ComponentModel.DataAnnotations;
using EventBooking.API.Validators;

namespace EventBooking.API.Entities;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Event title is required.")]
    [StringLength(200, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [FutureDate(ErrorMessage = "Event date must be in the future.")]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(500)]
    public string Location { get; set; } = string.Empty;

    [Range(1, 10000, ErrorMessage = "Available seats must be between 1 and 10,000.")]
    public int AvailableSeats { get; set; }

    public string ImageUrl { get; set; } = string.Empty;   // bonus field
    public decimal TicketPrice { get; set; }               // bonus field
    public string Category { get; set; } = string.Empty;   // bonus field (Music, Tech, Sports...)
    public bool IsActive { get; set; } = true;

    // Navigation
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}