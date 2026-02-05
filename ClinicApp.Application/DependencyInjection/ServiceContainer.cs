using ClinicApp.Application.Common;
using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.Interfaces;
using ClinicApp.Application.Services;
using ClinicApp.Application.Validation.Authentication;
using ClinicApp.Application.Validation.Doctor;
using ClinicApp.Application.Validation.Patient;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Application.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IDoctorService, DoctorService>();


            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
            services.AddScoped<IValidationsService, ValidationsService>();
            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
