using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(HospitalDbContext context) : base(context) { }

    public async Task<IEnumerable<Prescription>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(p => p.Appointment).ThenInclude(a => a.Patient)
            .Include(p => p.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .OrderByDescending(p => p.PrescriptionId)
            .ToListAsync();

    public async Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId) =>
        await _dbSet
            .Include(p => p.Appointment).ThenInclude(a => a.Patient)
            .Include(p => p.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .Where(p => p.Appointment.PatientId == patientId)
            .OrderByDescending(p => p.PrescriptionId)
            .ToListAsync();

    public async Task<Prescription?> GetByAppointmentIdAsync(int appointmentId) =>
        await _dbSet
            .Include(p => p.Appointment).ThenInclude(a => a.Patient)
            .Include(p => p.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .FirstOrDefaultAsync(p => p.AppointmentId == appointmentId);
}