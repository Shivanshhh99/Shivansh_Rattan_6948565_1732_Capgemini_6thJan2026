using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using SmartHospital.API.DTOs.Prescription;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PrescriptionsController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<PrescriptionsController> _logger;

    public PrescriptionsController(IUnitOfWork uow, IMapper mapper, ILogger<PrescriptionsController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var prescriptions = await _uow.Prescriptions.GetAllWithDetailsAsync();
        return Ok(_mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions));
    }

    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Patient,Admin")]
    public async Task<IActionResult> GetPatientPrescriptions(int patientId)
    {
        var prescriptions = await _uow.Prescriptions.GetPatientPrescriptionsAsync(patientId);
        return Ok(_mapper.Map<IEnumerable<PrescriptionDto>>(prescriptions));
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var prescription = await _uow.Prescriptions.GetByAppointmentIdAsync(appointmentId);
        if (prescription == null) throw new NotFoundException("Prescription for appointment", appointmentId);
        return Ok(_mapper.Map<PrescriptionDto>(prescription));
    }

    [HttpGet("{id}/download")]
    [Authorize(Roles = "Patient,Admin,Doctor")]
    public async Task<IActionResult> Download(int id)
    {
        var prescription = await _uow.Prescriptions.GetByIdAsync(id);
        if (prescription == null) throw new NotFoundException("Prescription", id);

        var detailedPrescription = await _uow.Prescriptions.GetByAppointmentIdAsync(prescription.AppointmentId);
        if (detailedPrescription == null) throw new NotFoundException("Prescription", id);

        var content = new StringBuilder();
        content.AppendLine("SMART HOSPITAL - PRESCRIPTION");
        content.AppendLine($"Prescription ID: {detailedPrescription.PrescriptionId}");
        content.AppendLine($"Appointment ID: {detailedPrescription.AppointmentId}");
        content.AppendLine($"Patient Name: {detailedPrescription.Appointment.Patient.FullName}");
        content.AppendLine($"Doctor Name: {detailedPrescription.Appointment.Doctor.User.FullName}");
        content.AppendLine($"Appointment Date: {detailedPrescription.Appointment.AppointmentDate:dd-MMM-yyyy HH:mm}");
        content.AppendLine();
        content.AppendLine($"Diagnosis: {detailedPrescription.Diagnosis}");
        content.AppendLine($"Medicines: {detailedPrescription.Medicines}");
        content.AppendLine($"Notes: {detailedPrescription.Notes}");

        var bytes = Encoding.UTF8.GetBytes(content.ToString());
        var fileName = $"prescription-{detailedPrescription.PrescriptionId}.txt";
        return File(bytes, "text/plain", fileName);
    }

    [HttpPost]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> Create([FromBody] CreatePrescriptionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var appt = await _uow.Appointments.GetAppointmentWithDetailsAsync(dto.AppointmentId);
        if (appt == null) throw new NotFoundException("Appointment", dto.AppointmentId);

        var existing = await _uow.Prescriptions.GetByAppointmentIdAsync(dto.AppointmentId);
        if (existing != null)
            throw new InvalidOperationException("Prescription already exists for this appointment.");

        var prescription = _mapper.Map<Prescription>(dto);
        await _uow.Prescriptions.AddAsync(prescription);

        // Mark appointment as completed
        appt.Status = "Completed";
        await _uow.Appointments.UpdateAsync(appt);

        await _uow.SaveChangesAsync();
        _logger.LogInformation("Prescription created for appointment {Id}", dto.AppointmentId);
        return Ok(prescription);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor,Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePrescriptionDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var prescription = await _uow.Prescriptions.GetByIdAsync(id);
        if (prescription == null) throw new NotFoundException("Prescription", id);

        prescription.Diagnosis = dto.Diagnosis;
        prescription.Medicines = dto.Medicines;
        prescription.Notes = dto.Notes;

        await _uow.Prescriptions.UpdateAsync(prescription);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}