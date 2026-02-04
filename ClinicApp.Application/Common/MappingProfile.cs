using AutoMapper;
using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.DTOs.User;
using ClinicApp.Application.DTOs.Specialty;
using ClinicApp.Domain.Models;
using ClinicApp.Application.DTOs.Doctor;

namespace ClinicApp.Application.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Patient mappings
            
            
            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();

            CreateMap<Patient, GetPatientDto>()
                .ForMember(d => d.PatientId, opt => opt.MapFrom(s => s.PatientId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User != null ? s.User.FullName : string.Empty))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User != null ? s.User.Email : string.Empty))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.User != null ? s.User.Phone ?? string.Empty : string.Empty))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.User != null && s.User.IsActive))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(s => s.BirthDate))
                .ForMember(d => d.Gender, opt => opt.MapFrom(s => s.Gender ?? string.Empty))
                .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address ?? string.Empty));

            // User mappings
            CreateMap<User, GetUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            // Specialty mappings
            CreateMap<Specialty, GetSpecialtyDto>();
            CreateMap<CreateSpecialtyDto, Specialty>();
            CreateMap<UpdateSpecialtyDto, Specialty>();

            // Doctor mappings
            CreateMap<CreateDoctorDto, Doctor>();
            CreateMap<UpdateDoctorDto, Doctor>();
            CreateMap<Doctor, GetDoctorDto>()
                .ForMember(d => d.DoctorId, opt => opt.MapFrom(s => s.DoctorId))
                 .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.UserId))
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.User != null ? s.User.FullName : string.Empty))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User != null ? s.User.Email : string.Empty))
                .ForMember(d => d.Phone, opt => opt.MapFrom(s => s.User != null ? s.User.Phone ?? string.Empty : string.Empty))
                .ForMember(d => d.IsActive, opt => opt.MapFrom(s => s.User != null && s.User.IsActive))
                .ForMember(d => d.SpecialtyName, opt => opt.MapFrom(s => s.Specialty != null ? s.Specialty.SpecialtyName : string.Empty));
        }
    }
}
