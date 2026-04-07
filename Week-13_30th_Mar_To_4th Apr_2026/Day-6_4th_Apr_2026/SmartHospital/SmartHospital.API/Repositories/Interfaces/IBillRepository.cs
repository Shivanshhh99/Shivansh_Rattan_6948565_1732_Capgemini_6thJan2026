using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IBillRepository : IGenericRepository<Bill>
{
    Task<IEnumerable<Bill>> GetAllWithDetailsAsync();
    Task<Bill?> GetByAppointmentIdAsync(int appointmentId);
    Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId);
    Task<IEnumerable<Bill>> GetUnpaidBillsAsync();
}