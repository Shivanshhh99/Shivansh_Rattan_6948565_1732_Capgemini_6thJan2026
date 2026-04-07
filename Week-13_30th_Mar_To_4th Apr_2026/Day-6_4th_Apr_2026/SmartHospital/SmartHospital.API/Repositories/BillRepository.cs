using Microsoft.EntityFrameworkCore;
using SmartHospital.API.Data;
using SmartHospital.API.Models;
using SmartHospital.API.Repositories.Interfaces;

namespace SmartHospital.API.Repositories;

public class BillRepository : GenericRepository<Bill>, IBillRepository
{
    public BillRepository(HospitalDbContext context) : base(context) { }

    public async Task<Bill?> GetByAppointmentIdAsync(int appointmentId) =>
        await _dbSet
            .Include(b => b.Appointment).ThenInclude(a => a.Patient)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);

    public async Task<IEnumerable<Bill>> GetByPatientIdAsync(int patientId) =>
        await _dbSet
            .Include(b => b.Appointment).ThenInclude(a => a.Patient)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .Where(b => b.Appointment.PatientId == patientId)
            .OrderByDescending(b => b.BillId)
            .ToListAsync();

    public async Task<IEnumerable<Bill>> GetAllWithDetailsAsync() =>
        await _dbSet
            .Include(b => b.Appointment).ThenInclude(a => a.Patient)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .OrderByDescending(b => b.BillId)
            .ToListAsync();

    public async Task<IEnumerable<Bill>> GetUnpaidBillsAsync() =>
        await _dbSet
            .Include(b => b.Appointment).ThenInclude(a => a.Patient)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .Where(b => b.PaymentStatus == "Unpaid")
            .ToListAsync();
}