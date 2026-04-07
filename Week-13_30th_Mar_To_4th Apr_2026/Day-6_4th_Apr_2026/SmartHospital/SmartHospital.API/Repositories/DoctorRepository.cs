using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(HospitalDbContext context) : base(context) { }

    public async Task<IEnumerable<Doctor>> GetDoctorsByDepartmentAsync(int departmentId) =>
        await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .Where(d => d.DepartmentId == departmentId)
            .ToListAsync();

    public async Task<Doctor?> GetDoctorWithDetailsAsync(int doctorId) =>
        await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.DoctorId == doctorId);

    public async Task<IEnumerable<Doctor>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(d => d.User)
            .Include(d => d.Department)
            .ToListAsync();
}