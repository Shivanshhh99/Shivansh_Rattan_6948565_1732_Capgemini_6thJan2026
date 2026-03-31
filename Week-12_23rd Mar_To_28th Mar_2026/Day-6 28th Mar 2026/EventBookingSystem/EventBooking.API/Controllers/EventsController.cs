using AutoMapper;
using EventBooking.API.Data;
using EventBooking.API.DTOs;
using EventBooking.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public EventsController(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // GET api/events
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? category, [FromQuery] string? search)
    {
        var query = _db.Events.Where(e => e.IsActive).AsQueryable();

        if (!string.IsNullOrWhiteSpace(category))
            query = query.Where(e => e.Category == category);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(e => e.Title.Contains(search) || e.Location.Contains(search));

        var events = await query.OrderBy(e => e.Date).ToListAsync();
        return Ok(_mapper.Map<List<EventDto>>(events));
    }

    // GET api/events/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ev = await _db.Events.FindAsync(id);
        if (ev == null || !ev.IsActive) return NotFound();
        return Ok(_mapper.Map<EventDto>(ev));
    }

    // POST api/events  (admin use, no auth guard for assessment simplicity)
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateEventDto dto)
    {
        var ev = _mapper.Map<Event>(dto);
        _db.Events.Add(ev);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = ev.Id }, _mapper.Map<EventDto>(ev));
    }
}