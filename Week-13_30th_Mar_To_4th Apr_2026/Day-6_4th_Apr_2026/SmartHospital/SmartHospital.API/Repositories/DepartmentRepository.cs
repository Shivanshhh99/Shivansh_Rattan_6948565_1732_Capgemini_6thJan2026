using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(HospitalDbContext context) : base(context) { }

    public async Task<Department?> GetWithDoctorsAsync(int departmentId) =>
        await _dbSet
            .Include(d => d.Doctors)
            .ThenInclude(doc => doc.User)
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
}