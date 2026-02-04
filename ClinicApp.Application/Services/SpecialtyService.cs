using AutoMapper;
using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.Specialty;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Services
{
    public class SpecialtyService(ISpecialtyRepository specialtyRepository,
        IMapper mapper, ILogger<SpecialtyService> logger) : ISpecialtyService
    {
        public async Task<ServiceResponse<IEnumerable<GetSpecialtyDto>>> GetAllSpecialtiesAsync()
        {
            try
            {
                var specialties = await specialtyRepository.GetAllAsync();
                var specialtyDtos = mapper.Map<IEnumerable<GetSpecialtyDto>>(specialties);
                return new ServiceResponse<IEnumerable<GetSpecialtyDto>>(specialtyDtos, true, "Specialties retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving all specialties.");
                return new ServiceResponse<IEnumerable<GetSpecialtyDto>>(null, false, "Error retrieving all specialties.");
            }

        }

        public async Task<ServiceResponse<GetSpecialtyDto>> GetSpecialtyByIdAsync(int id)
        {
            try
            {

                var specialty = await specialtyRepository.GetByIdAsync(id);
                if (specialty == null)
                    return new ServiceResponse<GetSpecialtyDto>(null, false, "Specialty not found");

                var specialtyDto = mapper.Map<GetSpecialtyDto>(specialty);
                return new ServiceResponse<GetSpecialtyDto>(specialtyDto, true, "Specialty retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving specialty.");
                return new ServiceResponse<GetSpecialtyDto>(null, false, "Error retrieving specialty.");
            }

        }

        public async Task<ServiceResponse> CreateSpecialtyAsync(CreateSpecialtyDto specialtyDto)
        {
            var specialty = mapper.Map<Specialty>(specialtyDto);
            await specialtyRepository.AddAsync(specialty);

            var createdSpecialtyDto = mapper.Map<GetSpecialtyDto>(specialty);
            return new ServiceResponse(true, "Specialty created successfully");
        }

        public async Task<ServiceResponse<GetSpecialtyDto>> UpdateSpecialtyAsync(int id, UpdateSpecialtyDto specialtyDto)
        {
            var specialty = await specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
                return new ServiceResponse<GetSpecialtyDto>(null, false, "Specialty not found");

            mapper.Map(specialtyDto, specialty);
            await specialtyRepository.UpdateAsync(specialty);

            var updatedSpecialtyDto = mapper.Map<GetSpecialtyDto>(specialty);
            return new ServiceResponse<GetSpecialtyDto>(updatedSpecialtyDto, true, "Specialty updated successfully");
        }

        public async Task<ServiceResponse> DeleteSpecialtyAsync(int id)
        {
            var specialty = await specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
                return new ServiceResponse(false, "Specialty not found");

           
            var inUse = await specialtyRepository.IsInUseAsync(id);

            if (inUse)
            {
                return new ServiceResponse(false, "Cannot delete specialty because one or more doctors are assigned to it.");
            }

            await specialtyRepository.DeleteAsync(id);
            return new ServiceResponse(true, "Specialty deleted successfully");
        }
    }
}
