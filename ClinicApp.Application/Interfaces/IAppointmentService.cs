using ClinicApp.Application.DTOs.Appointment;

namespace ClinicApp.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<GetAppointmentDto?> GetAppointmentByIdAsync(int id);
        Task<IEnumerable<GetAppointmentDto>> GetAppointmentsByPatientIdAsync(int patientId);
        Task<IEnumerable<GetAppointmentDto>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<GetAppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointmentDto);
        Task UpdateAppointmentAsync(int id, UpdateAppointmentDto appointmentDto);
        Task CancelAppointmentAsync(int id);
        Task ConfirmAppointmentAsync(int id);
        Task CompleteAppointmentAsync(int id);
        Task MarkNoShowAsync(int id);

        // New: get available appointment slots for a doctor for the week starting at weekStart (weekStart per stored-proc rules)
        Task<IEnumerable<GetAvailableSlotDto>> GetDoctorAvailableSlotsWeeklyAsync(int doctorId, DateTime weekStart);
    }
}
