using AutoMapper;
using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Doctor;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Services
{
    public class DoctorService(IDoctorRepository doctorRepository, 
        IUserRepository userRepository, IMapper mapper,
        ILogger<DoctorService> logger) : IDoctorService
    {
        public async Task<ServiceResponse<GetDoctorDto>> GetDoctorByIdAsync(int id)
        {
            try
            {
                var doctor = await doctorRepository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return new ServiceResponse<GetDoctorDto>(null, false, "Doctor not found.");
                }
                var doctorDto = mapper.Map<GetDoctorDto>(doctor);
                return new ServiceResponse<GetDoctorDto>(doctorDto, true, "Doctor retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving doctor by id.");
                return new ServiceResponse<GetDoctorDto>(null, false, "Error retrieving doctor by id.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetDoctorDto>>> GetAllDoctorsAsync()
        {
            try
            {
                var doctors = await doctorRepository.GetAllAsync();
                var doctorsDto = mapper.Map<IEnumerable<GetDoctorDto>>(doctors);
                return new ServiceResponse<IEnumerable<GetDoctorDto>>(doctorsDto, true, "Doctors retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving all doctors.");
                return new ServiceResponse<IEnumerable<GetDoctorDto>>(null, false, "Error retrieving all doctors.");
            }
        }

        public async Task<ServiceResponse> CreateDoctorAsync(CreateDoctorDto doctorDto)
        {
            try
            {
                var existingUser = await userRepository.GetByEmailAsync(doctorDto.Email);
                if (existingUser != null)
                {
                    return new ServiceResponse(false, "User with this email already exists.");
                }

                var user = new User
                {
                    FullName = doctorDto.FullName,
                    Email = doctorDto.Email,
                    Phone = doctorDto.Phone,
                    PasswordHash = doctorDto.Password, // TODO: hash the password
                    Role = "Doctor",
                    IsActive = doctorDto.IsActive
                };

                var doctor = mapper.Map<Doctor>(doctorDto);
                doctor.User = user;

                await userRepository.AddAsync(user);
                await doctorRepository.AddAsync(doctor);
                
                await doctorRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Doctor and user created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating doctor.");
                return new ServiceResponse(false, "Error creating doctor.");
            }
        }

        public async Task<ServiceResponse> UpdateDoctorAsync(int id, UpdateDoctorDto doctorDto)
        {
            try
            {
               

                var doctor = await doctorRepository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return new ServiceResponse(false, "Doctor not found.");
                }
                
                var user = doctor.User;
                if (user == null)
                {
                    return new ServiceResponse(false, "Associated user not found for doctor.");
                }

                var userWithEmail = await userRepository.GetByEmailAsync(doctorDto.Email);
                if (userWithEmail != null && userWithEmail.Id != user.Id)
                {
                    return new ServiceResponse(false, "Another user with this email already exists.");
                }

                mapper.Map(doctorDto, doctor);
                
                user.FullName = doctorDto.FullName;
                user.Email = doctorDto.Email;
                user.Phone = doctorDto.Phone;
                user.IsActive = doctorDto.IsActive;

                userRepository.Update(user);
                doctorRepository.Update(doctor);
                
                await doctorRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Doctor and user updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating doctor.");
                return new ServiceResponse(false, "Error updating doctor.");
            }
        }

        public async Task<ServiceResponse> DeleteDoctorAsync(int id)
        {
            try
            {
                var doctor = await doctorRepository.GetByIdAsync(id);
                if (doctor == null)
                {
                    return new ServiceResponse(false, "Doctor not found.");
                }

                var user = await userRepository.GetByIdAsync(doctor.UserId);
                if (user != null)
                {
                    userRepository.Delete(user);
                }

                doctorRepository.Delete(doctor);
                await doctorRepository.SaveChangesAsync();
                return new ServiceResponse(true, "Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting doctor.");
                return new ServiceResponse(false, "Error deleting doctor.");
            }
        }
    }
}
