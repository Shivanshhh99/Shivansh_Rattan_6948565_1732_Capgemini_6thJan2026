using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IPrescriptionRepository : IGenericRepository<Prescription>
{
    Task<IEnumerable<Prescription>> GetAllWithDetailsAsync();
    Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId);
    Task<Prescription?> GetByAppointmentIdAsync(int appointmentId);
}