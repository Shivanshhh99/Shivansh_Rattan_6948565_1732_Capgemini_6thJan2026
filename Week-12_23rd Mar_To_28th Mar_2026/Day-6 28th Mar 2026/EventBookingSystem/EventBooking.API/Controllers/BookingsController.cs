using System.Security.Claims;
using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]                    // All booking endpoints require JWT
public class BookingsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public BookingsController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // GET api/bookings  (user's own bookings)
    [HttpGet]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var bookings = await _db.Bookings
            .Include(b => b.Event)
            .Where(b => b.UserId == userId && b.Status != BookingStatus.Cancelled)
            .OrderByDescending(b => b.BookedAt)
            .ToListAsync();

        return Ok(_mapper.Map<List<BookingDto>>(bookings));
    }

    // POST api/bookings
    [HttpPost]
    public async Task<IActionResult> Book(CreateBookingDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var ev = await _db.Events.FindAsync(dto.EventId);
        if (ev == null || !ev.IsActive)
            return NotFound("Event not found.");

        if (ev.AvailableSeats < dto.SeatsBooked)
            return BadRequest($"Only {ev.AvailableSeats} seats remaining.");

        // Check duplicate booking
        var existing = await _db.Bookings.FirstOrDefaultAsync(b =>
            b.EventId == dto.EventId && b.UserId == userId &&
            b.Status == BookingStatus.Confirmed);

        if (existing != null)
            return Conflict("You already have a booking for this event.");

        // Deduct seats
        ev.AvailableSeats -= dto.SeatsBooked;

        var booking = new Booking
        {
            EventId = dto.EventId,
            UserId = userId,
            SeatsBooked = dto.SeatsBooked,
            Status = BookingStatus.Confirmed
        };

        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();

        // Return with event details
        await _db.Entry(booking).Reference(b => b.Event).LoadAsync();
        return CreatedAtAction(nameof(GetMyBookings), _mapper.Map<BookingDto>(booking));
    }

    // DELETE api/bookings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        var booking = await _db.Bookings
            .Include(b => b.Event)
            .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);

        if (booking == null) return NotFound();
        if (booking.Status == BookingStatus.Cancelled)
            return BadRequest("Booking is already cancelled.");

        // Restore seats
        booking.Event.AvailableSeats += booking.SeatsBooked;
        booking.Status = BookingStatus.Cancelled;

        await _db.SaveChangesAsync();
        return NoContent();
    }
}