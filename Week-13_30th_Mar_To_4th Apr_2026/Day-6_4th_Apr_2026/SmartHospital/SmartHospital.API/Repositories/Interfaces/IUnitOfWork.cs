namespace SmartHospital.API.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IDoctorRepository Doctors { get; }
    IDepartmentRepository Departments { get; }
    IAppointmentRepository Appointments { get; }
    IPrescriptionRepository Prescriptions { get; }
    IBillRepository Bills { get; }
    Task<int> SaveChangesAsync();
}