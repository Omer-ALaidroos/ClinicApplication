using AutoMapper;
using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.DTOs.User;
using ClinicApp.Application.Interfaces;
using ClinicApp.Application.Validation.Authentication;
using ClinicApp.Application.Validation.Patient;
using ClinicApp.Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Services
{
    public class PatientService(IPatientRepository patientRepository,
        IUserRepository userRepository, IMapper mapper,
        ILogger<PatientService> logger, IValidator<CreatePatientDto> createPatientValidator, 
        IValidationsService validationsService) : IPatientService
    {
        public async Task<ServiceResponse<GetPatientDto>> GetPatientByIdAsync(int id)
        {
            try
            {
                var patient = await patientRepository.GetByIdAsync(id);
                if (patient == null)
                {
                    return new ServiceResponse<GetPatientDto>(null, false, "Patient not found.");
                }
                var patientDto = mapper.Map<GetPatientDto>(patient);
                return new ServiceResponse<GetPatientDto>(patientDto, true, "Patient retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving patient by id.");
                return new ServiceResponse<GetPatientDto>(null, false, "Error retrieving patient by id.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetPatientDto>>> GetAllPatientsAsync()
        {
            try
            {
                var patients = await patientRepository.GetAllAsync();
                var patientsDto = mapper.Map<IEnumerable<GetPatientDto>>(patients);
                return new ServiceResponse<IEnumerable<GetPatientDto>>(patientsDto, true, "Patients retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving all patients.");
                return new ServiceResponse<IEnumerable<GetPatientDto>>(null, false, "Error retrieving all patients.");
            }
        }

        public async Task<ServiceResponse> CreatePatientAsync(CreatePatientDto patientDto)
        {
            var validationResult = await validationsService.ValidateAsync<CreatePatientDto>(patientDto, createPatientValidator);
            if (!validationResult.Success) return validationResult;
            try
            {
                // Check if a user with the same email already exists
                var existingUser = await userRepository.GetByEmailAsync(patientDto.Email);
                if (existingUser != null)
                {
                    return new ServiceResponse(false, "User with this email already exists.");
                }

                // Create user from patient DTO (password should be hashed in production)
                var user = new User
                {
                    FullName = patientDto.FullName,
                    Email = patientDto.Email,
                    Phone = patientDto.Phone,
                    PasswordHash = patientDto.Password, // TODO: hash the password
                    Role = "Patient",
                    IsActive = patientDto.IsActive
                };

                
                var patient = mapper.Map<Patient>(patientDto);
                patient.User = user;

                await userRepository.AddAsync(user);
                await patientRepository.AddAsync(patient);

              
                await patientRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Patient and user created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating patient.");
                return new ServiceResponse(false, "Error creating patient.");
            }
        }

        public async Task<ServiceResponse> UpdatePatientAsync(int id, UpdatePatientDto patientDto)
        {
            try
            {
                var patient = await patientRepository.GetByIdAsync(id);
                if (patient == null)
                {
                    return new ServiceResponse(false, "Patient not found.");
                }

              
                var user = await userRepository.GetByIdAsync(patient.UserId);
                if (user == null)
                {
                    
                    return new ServiceResponse(false, "Associated user not found for patient.");
                }

               
                var userWithEmail = await userRepository.GetByEmailAsync(patientDto.Email);
                if (userWithEmail != null && userWithEmail.Id != user.Id)
                {
                    return new ServiceResponse(false, "Another user with this email already exists.");
                }

                
                mapper.Map(patientDto, patient);

              
                user.FullName = patientDto.FullName;
                user.Email = patientDto.Email;
                user.Phone = patientDto.Phone;
                user.IsActive = patientDto.IsActive;

                userRepository.Update(user);
                patientRepository.Update(patient);

              
                await patientRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Patient and user updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating patient.");
                return new ServiceResponse(false, "Error updating patient.");
            }
        }

        public async Task<ServiceResponse> DeletePatientAsync(int id)
        {
            try
            {
                var patient = await patientRepository.GetByIdAsync(id);
                if (patient == null)
                {
                    return new ServiceResponse(false, "Patient not found.");
                }
                patientRepository.Delete(patient);
                await patientRepository.SaveChangesAsync();
                return new ServiceResponse(true, "Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting patient.");
                return new ServiceResponse(false, "Error deleting patient.");
            }
        }
    }
}
