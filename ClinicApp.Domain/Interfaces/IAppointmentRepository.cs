using ClinicApp.Domain.Models;

namespace ClinicApp.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);

        // New: return appointments for a doctor on a specific date (based on StartDateTime)
        Task<IEnumerable<Appointment>> GetAppointmentsForDoctorByDateAsync(int doctorId, DateTime date);
         Task<IEnumerable<AvailableAppointmentSlot>> GetDoctorAvailableSlotsWeeklyAsync(int doctorId, DateTime weekStart);
        Task<Appointment> AddAsync(Appointment appointment);
        void Update(Appointment appointment);
        Task SaveChangesAsync();
    }
}
