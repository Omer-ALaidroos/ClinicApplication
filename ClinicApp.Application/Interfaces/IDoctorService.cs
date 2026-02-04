using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Doctor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<ServiceResponse<GetDoctorDto>> GetDoctorByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<GetDoctorDto>>> GetAllDoctorsAsync();
        Task<ServiceResponse> CreateDoctorAsync(CreateDoctorDto doctorDto);
        Task<ServiceResponse> UpdateDoctorAsync(int id, UpdateDoctorDto doctorDto);
        Task<ServiceResponse> DeleteDoctorAsync(int id);
    }
}
