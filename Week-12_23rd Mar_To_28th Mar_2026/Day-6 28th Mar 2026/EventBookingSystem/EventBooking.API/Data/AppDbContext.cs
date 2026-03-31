using EventBooking.API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Booking>()
            .HasOne(b => b.Event)
            .WithMany(e => e.Bookings)
            .HasForeignKey(b => b.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data (STATIC values only)
        builder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Title = "ASP.NET Core Summit 2025",
                Description = "A deep-dive conference covering .NET 9 features, cloud-native apps, and more.",
                Date = new DateTime(2026, 6, 1, 10, 0, 0),
                Location = "New Delhi Convention Centre",
                AvailableSeats = 300,
                TicketPrice = 1499,
                Category = "Technology",
                ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=800"
            },
            new Event
            {
                Id = 2,
                Title = "Sunburn Music Festival",
                Description = "India's premier electronic music festival featuring world-class DJs.",
                Date = new DateTime(2026, 7, 15, 18, 0, 0),
                Location = "Pune, Maharashtra",
                AvailableSeats = 5000,
                TicketPrice = 2999,
                Category = "Music",
                ImageUrl = "https://images.unsplash.com/photo-1470229722913-7c0e2dbbafd3?w=800"
            },
            new Event
            {
                Id = 3,
                Title = "Startup Pitch Day",
                Description = "Watch 20 startups pitch to leading investors. Network with founders.",
                Date = new DateTime(2026, 5, 10, 9, 30, 0),
                Location = "Bangalore Tech Park",
                AvailableSeats = 150,
                TicketPrice = 499,
                Category = "Business",
                ImageUrl = "https://images.unsplash.com/photo-1559136555-9303baea8ebd?w=800"
            }
        );
    }
}