using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IDepartmentRepository : IGenericRepository<Department>
{
    Task<Department?> GetWithDoctorsAsync(int departmentId);
}