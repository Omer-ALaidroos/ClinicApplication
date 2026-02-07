using AutoMapper;
using ClinicApp.Application.DTOs;
using ClinicApp.Application.DTOs.DoctorSchedule;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Services
{
    public class DoctorScheduleService(IDoctorScheduleRepository doctorScheduleRepository,
        IDoctorRepository doctorRepository, IMapper mapper, ILogger<DoctorScheduleService> logger) : IDoctorScheduleService
    {
        public async Task<ServiceResponse<GetDoctorScheduleDto>> GetDoctorScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await doctorScheduleRepository.GetByIdAsync(id);
                if (schedule == null)
                {
                    return new ServiceResponse<GetDoctorScheduleDto>(null, false, "Doctor schedule not found.");
                }
                var dto = mapper.Map<GetDoctorScheduleDto>(schedule);
                return new ServiceResponse<GetDoctorScheduleDto>(dto, true, "Doctor schedule retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving doctor schedule by id.");
                return new ServiceResponse<GetDoctorScheduleDto>(null, false, "Error retrieving doctor schedule by id.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetDoctorScheduleDto>>> GetAllDoctorSchedulesAsync()
        {
            try
            {
                var schedules = await doctorScheduleRepository.GetAllAsync();
                var dtos = mapper.Map<IEnumerable<GetDoctorScheduleDto>>(schedules);
                return new ServiceResponse<IEnumerable<GetDoctorScheduleDto>>(dtos, true, "Doctor schedules retrieved successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving doctor schedules.");
                return new ServiceResponse<IEnumerable<GetDoctorScheduleDto>>(null, false, "Error retrieving doctor schedules.");
            }
        }

        public async Task<ServiceResponse> CreateDoctorScheduleAsync(CreateDoctorScheduleDto dto)
        {
            try
            {
                var doctor = await doctorRepository.GetByIdAsync(dto.DoctorId);
                if (doctor == null)
                {
                    return new ServiceResponse(false, "Doctor not found.");
                }

                if(doctorScheduleRepository.IsDayReversedrActiveAsync(dto.DoctorId,dto.DayOfWeek))
                {
                    return new ServiceResponse(false, "this day found already.");
                }

                var schedule = mapper.Map<DoctorSchedule>(dto);
                await doctorScheduleRepository.AddAsync(schedule);
                await doctorScheduleRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Doctor schedule created successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating doctor schedule.");
                return new ServiceResponse(false, "Error creating doctor schedule.");
            }
        }

        public async Task<ServiceResponse> UpdateDoctorScheduleAsync(int id, UpdateDoctorScheduleDto dto)
        {
            try
            {
                var schedule = await doctorScheduleRepository.GetByIdAsync(id);
                if (schedule == null)
                {
                    return new ServiceResponse(false, "Doctor schedule not found.");
                }

                mapper.Map(dto, schedule);

                doctorScheduleRepository.Update(schedule);
                await doctorScheduleRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Doctor schedule updated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating doctor schedule.");
                return new ServiceResponse(false, "Error updating doctor schedule.");
            }
        }

        public async Task<ServiceResponse> DeleteDoctorScheduleAsync(int id)
        {
            try
            {
                var schedule = await doctorScheduleRepository.GetByIdAsync(id);
                if (schedule == null)
                {
                    return new ServiceResponse(false, "Doctor schedule not found.");
                }

                doctorScheduleRepository.Delete(schedule);
                await doctorScheduleRepository.SaveChangesAsync();

                return new ServiceResponse(true, "Doctor schedule deleted successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting doctor schedule.");
                return new ServiceResponse(false, "Error deleting doctor schedule.");
            }
        }

        public async Task<ServiceResponse<IEnumerable<GetDoctorScheduleDto>>> GetSchedulesForDoctorAsync(int doctorId)
        {
            try
            {
                var schedules = await doctorScheduleRepository.GetSchedulesForDoctorAsync(doctorId);
                var dtos = mapper.Map<IEnumerable<GetDoctorScheduleDto>>(schedules);
                return new ServiceResponse<IEnumerable<GetDoctorScheduleDto>>(dtos, true, "Schedules retrieved.");
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex, "Error getting schedules for doctor {DoctorId}", doctorId);
                return new ServiceResponse<IEnumerable<GetDoctorScheduleDto>>(null, false, "Error getting schedules for doctor.");
            }
        }
    }
}
