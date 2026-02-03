using AutoMapper;
using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.User;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicApp.Application.Services
{
    public class UserService(IUserRepository userRepository,
        IMapper mapper, ILogger<UserService> logger) : IUserService
    {
        public async Task<ServiceResponse<GetUserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ServiceResponse<GetUserDto>(null, false, "User not found.");
                }
                var userDto = mapper.Map<GetUserDto>(user);
                return new ServiceResponse<GetUserDto>(userDto, true, "User retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving user by id.");
                return new ServiceResponse<GetUserDto>(null, false, "Error retrieving user by id.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await userRepository.GetAllAsync();
                var usersDto = mapper.Map<IEnumerable<GetUserDto>>(users);
                return new ServiceResponse<IEnumerable<GetUserDto>>(usersDto, true, "Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving all users.");
                return new ServiceResponse<IEnumerable<GetUserDto>>(null, false, "Error retrieving all users.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsersByRoleAsync(string role)
        {
            try
            {
                var users = await userRepository.GetAllAsync();
                var usersByRole = users.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));
                var usersDto = mapper.Map<IEnumerable<GetUserDto>>(usersByRole);
                return new ServiceResponse<IEnumerable<GetUserDto>>(usersDto, true, "Users retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving users by role.");
                return new ServiceResponse<IEnumerable<GetUserDto>>(null, false, "Error retrieving users by role.");
            }
        }

        public async Task<ServiceResponse> CreateUserAsync(CreateUserDto userDto)
        {
            try
            {
                var existingUser = await userRepository.GetByEmailAsync(userDto.Email);
                if (existingUser != null)
                {
                    return new ServiceResponse( false, "User with this email already exists.");
                }

                var user = mapper.Map<User>(userDto);
                // In a real app, you would hash the password here.
                // For now, we'll store it as is, but this is not secure.
                user.PasswordHash = userDto.Password; 

                await userRepository.AddAsync(user);
               int result = await userRepository.SaveChangesAsync();
                if(result == 0)
                {
                    return new ServiceResponse(false, "Failed to create user.");
                }
                var userResponseDto = mapper.Map<GetUserDto>(user);
                return new ServiceResponse(true, "User created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating user.");
                return new ServiceResponse(false, "Error creating user.");
            }
        }

        public async Task<ServiceResponse> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ServiceResponse(false, "User not found.");
                }
                mapper.Map(userDto, user);
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();
                return new ServiceResponse(true, "User updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating user.");
                return new ServiceResponse(false, "Error updating user.");
            }
        }

        public async Task<ServiceResponse> DeleteUserAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return new ServiceResponse(false, "User not found.");
                }
                userRepository.Delete(user);
                await userRepository.SaveChangesAsync();
                return new ServiceResponse(true, "User deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting user.");
                return new ServiceResponse(false, "Error deleting user.");
            }
        }
    }
}
