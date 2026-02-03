using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastucture.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicAppContext _context;

        public AppointmentRepository(ClinicAppContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return appointment;
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
