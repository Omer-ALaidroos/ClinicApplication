using ClinicApp.Application.Interfaces;
using ClinicApp.Application.Interfaces.Logger;
using ClinicApp.Domain.Interfaces;
using ClinicApp.Infrastucture.Data;
using ClinicApp.Infrastucture.Repositories;
using ClinicApp.Infrastucture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Infrastucture.DependencyInjection
{
   public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ClinicAppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ClinicAppContext")));

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorScheduleRepository, DoctorScheduleRepository>();

            services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;

                var keyBytes = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
                var signingKey = new SymmetricSecurityKey(keyBytes);

                options.TokenValidationParameters = new TokenValidationParameters
                 {

                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,

                    ClockSkew = TimeSpan.Zero,
                     IssuerSigningKey = signingKey,

                    ValidAudience = configuration["JWT:Audience"],
                    ValidIssuer = configuration["JWT:Issuer"],

                   
                     
                   
                };

               
            }
            );


            return services;
        }
    }
}
