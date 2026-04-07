using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IDoctorRepository : IGenericRepository<Doctor>
{
    Task<IEnumerable<Doctor>> GetDoctorsByDepartmentAsync(int departmentId);
    Task<Doctor?> GetDoctorWithDetailsAsync(int doctorId);
    Task<IEnumerable<Doctor>> GetAllWithDetailsAsync();
}