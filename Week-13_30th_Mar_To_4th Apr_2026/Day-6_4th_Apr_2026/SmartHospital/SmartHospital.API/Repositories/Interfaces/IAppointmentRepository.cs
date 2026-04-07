using SmartHospital.API.Models;

namespace SmartHospital.API.Repositories.Interfaces;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetAllWithDetailsAsync();
    Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId);
    Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId);
    Task<Appointment?> GetAppointmentWithDetailsAsync(int appointmentId);
}