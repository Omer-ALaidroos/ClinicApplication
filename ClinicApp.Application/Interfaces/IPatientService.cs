using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Patient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface IPatientService
    {
        Task<ServiceResponse<GetPatientDto>> GetPatientByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<GetPatientDto>>> GetAllPatientsAsync();
        Task<ServiceResponse> CreatePatientAsync(CreatePatientDto patientDto);
        Task<ServiceResponse> UpdatePatientAsync(int id, UpdatePatientDto patientDto);
        Task<ServiceResponse> DeletePatientAsync(int id);
    }
}
