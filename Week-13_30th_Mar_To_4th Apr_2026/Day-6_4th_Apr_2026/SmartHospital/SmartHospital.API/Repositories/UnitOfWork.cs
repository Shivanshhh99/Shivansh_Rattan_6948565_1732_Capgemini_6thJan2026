using SmartHospital.API.Data;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HospitalDbContext _context;

    public IUserRepository Users { get; }
    public IDoctorRepository Doctors { get; }
    public IDepartmentRepository Departments { get; }
    public IAppointmentRepository Appointments { get; }
    public IPrescriptionRepository Prescriptions { get; }
    public IBillRepository Bills { get; }

    public UnitOfWork(HospitalDbContext context,
        IUserRepository users,
        IDoctorRepository doctors,
        IDepartmentRepository departments,
        IAppointmentRepository appointments,
        IPrescriptionRepository prescriptions,
        IBillRepository bills)
    {
        _context = context;
        Users = users;
        Doctors = doctors;
        Departments = departments;
        Appointments = appointments;
        Prescriptions = prescriptions;
        Bills = bills;
    }

    public async Task<int> SaveChangesAsync() =>
        await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}