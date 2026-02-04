using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Specialty;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface ISpecialtyService
    {
        Task<ServiceResponse<IEnumerable<GetSpecialtyDto>>> GetAllSpecialtiesAsync();
        Task<ServiceResponse<GetSpecialtyDto>> GetSpecialtyByIdAsync(int id);
        Task<ServiceResponse> CreateSpecialtyAsync(CreateSpecialtyDto specialtyDto);
        Task<ServiceResponse<GetSpecialtyDto>> UpdateSpecialtyAsync(int id, UpdateSpecialtyDto specialtyDto);
        Task<ServiceResponse> DeleteSpecialtyAsync(int id);
    }
}
