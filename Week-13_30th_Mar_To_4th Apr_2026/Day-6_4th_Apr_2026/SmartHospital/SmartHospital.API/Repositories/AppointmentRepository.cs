using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(HospitalDbContext context) : base(context) { }

    public async Task<IEnumerable<Appointment>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Doctor).ThenInclude(d => d.Department)
            .Include(a => a.Prescription)
            .Include(a => a.Bill)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetPatientAppointmentsAsync(int patientId) =>
        await _dbSet
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Doctor).ThenInclude(d => d.Department)
            .Include(a => a.Prescription)
            .Include(a => a.Bill)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<IEnumerable<Appointment>> GetDoctorAppointmentsAsync(int doctorId) =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Prescription)
            .Include(a => a.Bill)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

    public async Task<Appointment?> GetAppointmentWithDetailsAsync(int appointmentId) =>
        await _dbSet
            .Include(a => a.Patient)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Doctor).ThenInclude(d => d.Department)
            .Include(a => a.Prescription)
            .Include(a => a.Bill)
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);
}