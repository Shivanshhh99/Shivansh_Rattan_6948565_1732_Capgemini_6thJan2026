using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartHospital.API.Exceptions;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(IUnitOfWork uow, ILogger<DepartmentsController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _uow.Departments.GetAllAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dept = await _uow.Departments.GetWithDoctorsAsync(id);
        if (dept == null) throw new NotFoundException("Department", id);
        return Ok(dept);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] Department dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _uow.Departments.AddAsync(dto);
        await _uow.SaveChangesAsync();

        _logger.LogInformation("Department created: {Name}", dto.DepartmentName);
        return CreatedAtAction(nameof(GetById), new { id = dto.DepartmentId }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] Department dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var dept = await _uow.Departments.GetByIdAsync(id);
        if (dept == null) throw new NotFoundException("Department", id);

        dept.DepartmentName = dto.DepartmentName;
        dept.Description = dto.Description;

        await _uow.Departments.UpdateAsync(dept);
        await _uow.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var dept = await _uow.Departments.GetByIdAsync(id);
        if (dept == null) throw new NotFoundException("Department", id);

        await _uow.Departments.DeleteAsync(dept);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}