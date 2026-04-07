using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartHospital.API.DTOs.Billing;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BillingController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<BillingController> _logger;

    public BillingController(IUnitOfWork uow, IMapper mapper, ILogger<BillingController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var bills = await _uow.Bills.GetAllWithDetailsAsync();
        return Ok(_mapper.Map<IEnumerable<BillDto>>(bills));
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var bill = await _uow.Bills.GetByAppointmentIdAsync(appointmentId);
        if (bill == null) throw new NotFoundException("Bill for appointment", appointmentId);
        return Ok(_mapper.Map<BillDto>(bill));
    }

    [HttpGet("patient/{patientId}")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var role = User.FindFirstValue(ClaimTypes.Role);
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (role == "Patient" && (!int.TryParse(userIdClaim, out var userId) || userId != patientId))
            return Forbid();

        var bills = await _uow.Bills.GetByPatientIdAsync(patientId);
        return Ok(_mapper.Map<IEnumerable<BillDto>>(bills));
    }

    [HttpGet("unpaid")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUnpaid()
    {
        var bills = await _uow.Bills.GetUnpaidBillsAsync();
        return Ok(_mapper.Map<IEnumerable<BillDto>>(bills));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Create([FromBody] CreateBillDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var appt = await _uow.Appointments.GetByIdAsync(dto.AppointmentId);
        if (appt == null) throw new NotFoundException("Appointment", dto.AppointmentId);

        var existing = await _uow.Bills.GetByAppointmentIdAsync(dto.AppointmentId);
        if (existing != null)
        {
            existing.ConsultationFee = dto.ConsultationFee;
            existing.MedicineCharges = dto.MedicineCharges;
            await _uow.Bills.UpdateAsync(existing);
            await _uow.SaveChangesAsync();

            var updatedBill = await _uow.Bills.GetByAppointmentIdAsync(dto.AppointmentId);
            if (updatedBill == null) throw new NotFoundException("Bill for appointment", dto.AppointmentId);

            _logger.LogInformation("Bill updated for appointment {Id}, total: {Total}", dto.AppointmentId, updatedBill.TotalAmount);
            return Ok(_mapper.Map<BillDto>(updatedBill));
        }

        var bill = _mapper.Map<Bill>(dto);
        await _uow.Bills.AddAsync(bill);
        await _uow.SaveChangesAsync();

        var createdBill = await _uow.Bills.GetByAppointmentIdAsync(dto.AppointmentId);
        if (createdBill == null) throw new NotFoundException("Bill for appointment", dto.AppointmentId);

        _logger.LogInformation("Bill created for appointment {Id}, total: {Total}", dto.AppointmentId, createdBill.TotalAmount);
        return Ok(_mapper.Map<BillDto>(createdBill));
    }

    [HttpPut("{id}/pay")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<IActionResult> MarkAsPaid(int id)
    {
        var bill = await _uow.Bills.GetByIdAsync(id);
        if (bill == null) throw new NotFoundException("Bill", id);

        bill.PaymentStatus = "Paid";
        await _uow.Bills.UpdateAsync(bill);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}