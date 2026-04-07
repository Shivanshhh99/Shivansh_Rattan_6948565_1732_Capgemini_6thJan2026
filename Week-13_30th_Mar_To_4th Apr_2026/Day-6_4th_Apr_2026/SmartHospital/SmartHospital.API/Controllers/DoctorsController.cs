using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.DTOs.Doctor;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly ILogger<DoctorsController> _logger;

    public DoctorsController(IUnitOfWork uow, IMapper mapper, ILogger<DoctorsController> logger)
    {
        _uow = uow;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _uow.Doctors.GetAllWithDetailsAsync();
        return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _uow.Doctors.GetDoctorWithDetailsAsync(id);
        if (doctor == null) throw new NotFoundException("Doctor", id);
        return Ok(_mapper.Map<DoctorDto>(doctor));
    }

    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetByDepartment(int departmentId)
    {
        var doctors = await _uow.Doctors.GetDoctorsByDepartmentAsync(departmentId);
        return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var emailExists = await _uow.Users.GetByEmailAsync(dto.Email);
        if (emailExists != null) throw new DuplicateEmailException(dto.Email);

        var deptExists = await _uow.Departments.GetByIdAsync(dto.DepartmentId);
        if (deptExists == null) throw new NotFoundException("Department", dto.DepartmentId);

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Doctor"
        };
        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();

        var doctor = new Doctor
        {
            UserId = user.UserId,
            DepartmentId = dto.DepartmentId,
            Specialization = dto.Specialization,
            ExperienceYears = dto.ExperienceYears,
            Availability = dto.Availability,
            ConsultationFee = dto.ConsultationFee
        };
        await _uow.Doctors.AddAsync(doctor);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Doctor created: {Name}", dto.FullName);
        return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorId }, new { doctor.DoctorId });
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var doctor = await _uow.Doctors.GetDoctorWithDetailsAsync(id);
        if (doctor == null) throw new NotFoundException("Doctor", id);

        doctor.User.FullName = dto.FullName;
        doctor.DepartmentId = dto.DepartmentId;
        doctor.Specialization = dto.Specialization;
        doctor.ExperienceYears = dto.ExperienceYears;
        doctor.Availability = dto.Availability;
        doctor.ConsultationFee = dto.ConsultationFee;

        await _uow.Doctors.UpdateAsync(doctor);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _uow.Doctors.GetByIdAsync(id);
        if (doctor == null) throw new NotFoundException("Doctor", id);

        await _uow.Doctors.DeleteAsync(doctor);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}