using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs.Appointment;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<AppointmentsController> _logger;

    public AppointmentsController(IUnitOfWork uow, IMapper mapper, ILogger<AppointmentsController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _uow.Appointments.GetAllWithDetailsAsync();
        return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appointments));
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientAppointments(int patientId)
    {
        var appointments = await _uow.Appointments.GetPatientAppointmentsAsync(patientId);
        return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appointments));
    }

    [HttpGet("doctor/{doctorId}")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> GetDoctorAppointments(int doctorId)
    {
        var appointments = await _uow.Appointments.GetDoctorAppointmentsAsync(doctorId);
        return Ok(_mapper.Map<IEnumerable<AppointmentDto>>(appointments));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appt = await _uow.Appointments.GetAppointmentWithDetailsAsync(id);
        if (appt == null) throw new NotFoundException("Appointment", id);
        return Ok(_mapper.Map<AppointmentDto>(appt));
    }

    [HttpPost]
    [Authorize(Roles = "Patient,Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (dto.AppointmentDate <= DateTime.UtcNow)
            return BadRequest(new { message = "Appointment date must be in the future" });

        var doctorExists = await _uow.Doctors.GetByIdAsync(dto.DoctorId);
        if (doctorExists == null) throw new NotFoundException("Doctor", dto.DoctorId);

        var patientExists = await _uow.Users.GetByIdAsync(dto.PatientId);
        if (patientExists == null) throw new NotFoundException("Patient", dto.PatientId);

        // Check for time conflicts (within 30 minutes)
        var existingAppts = await _uow.Appointments.GetDoctorAppointmentsAsync(dto.DoctorId);
        var conflict = existingAppts.Any(a =>
            a.Status == "Booked" &&
            Math.Abs((a.AppointmentDate - dto.AppointmentDate).TotalMinutes) < 30);

        if (conflict) throw new AppointmentConflictException();

        var appointment = _mapper.Map<Appointment>(dto);
        appointment.Status = "Booked";

        await _uow.Appointments.AddAsync(appointment);

        // Auto-generate bill at booking time using doctor's consultation fee.
        var bill = new Bill
        {
            Appointment = appointment,
            ConsultationFee = doctorExists.ConsultationFee,
            MedicineCharges = 0,
            PaymentStatus = "Unpaid"
        };
        await _uow.Bills.AddAsync(bill);

        await _uow.SaveChangesAsync();

        _logger.LogInformation("Appointment booked: Patient {PatientId} with Doctor {DoctorId}", dto.PatientId, dto.DoctorId);
        return CreatedAtAction(nameof(GetById), new { id = appointment.AppointmentId }, new { appointment.AppointmentId });
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var validStatuses = new[] { "Booked", "Completed", "Cancelled" };
        if (!validStatuses.Contains(status))
            return BadRequest(new { message = "Invalid status value" });

        var appt = await _uow.Appointments.GetByIdAsync(id);
        if (appt == null) throw new NotFoundException("Appointment", id);

        appt.Status = status;
        await _uow.Appointments.UpdateAsync(appt);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var appt = await _uow.Appointments.GetByIdAsync(id);
        if (appt == null) throw new NotFoundException("Appointment", id);

        await _uow.Appointments.DeleteAsync(appt);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}