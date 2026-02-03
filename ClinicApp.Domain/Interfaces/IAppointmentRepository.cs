using ClinicApp.Domain.Models;

namespace ClinicApp.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment?> GetByIdAsync(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<Appointment> AddAsync(Appointment appointment);
        void Update(Appointment appointment);
        Task SaveChangesAsync();
    }
}
