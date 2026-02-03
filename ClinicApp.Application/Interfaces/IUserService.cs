using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<GetUserDto>> GetUserByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllUsersAsync();
        Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsersByRoleAsync(string role);
        Task<ServiceResponse> CreateUserAsync(CreateUserDto userDto);
        Task<ServiceResponse> UpdateUserAsync(int id, UpdateUserDto userDto);
        Task<ServiceResponse> DeleteUserAsync(int id);
    }
}
