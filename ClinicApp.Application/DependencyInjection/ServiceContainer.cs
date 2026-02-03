using ClinicApp.Application.Common;
using ClinicApp.Application.Interfaces;
using ClinicApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Infrastucture.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IUserService, UserService>();

            // Register AutoMapper
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            return services;
        }
    }
}
