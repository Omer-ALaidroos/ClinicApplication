using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.DoctorSchedule;
using ClinicApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task<ServiceResponse<GetDoctorScheduleDto>> GetDoctorScheduleByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<GetDoctorScheduleDto>>> GetAllDoctorSchedulesAsync();
        Task<ServiceResponse> CreateDoctorScheduleAsync(CreateDoctorScheduleDto dto);
        Task<ServiceResponse> UpdateDoctorScheduleAsync(int id, UpdateDoctorScheduleDto dto);
        Task<ServiceResponse> DeleteDoctorScheduleAsync(int id);
        Task<ServiceResponse<IEnumerable<GetDoctorScheduleDto>>> GetSchedulesForDoctorAsync(int doctorId);
    }
}
