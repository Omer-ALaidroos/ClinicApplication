using ClinicApp.Application.DTOs.DoctorSchedule;
using FluentValidation;
using System;

namespace ClinicApp.Application.Validation.DoctorSchedule
{
    public class UpdateDoctorScheduleValidator : AbstractValidator<UpdateDoctorScheduleDto>
    {
        public UpdateDoctorScheduleValidator()
        {
            RuleFor(x => (int)x.DayOfWeek)
                .InclusiveBetween(0, 6).WithMessage("DayOfWeek must be between 0 (Sunday) and 6 (Saturday).");

            RuleFor(x => x.StartTime)
                .Must(t => t != default(TimeOnly)).WithMessage("Start time is required.");

            RuleFor(x => x.EndTime)
                .Must(t => t != default(TimeOnly)).WithMessage("End time is required.");

            RuleFor(x => x)
                .Must(x => x.StartTime < x.EndTime)
                .WithMessage("StartTime must be earlier than EndTime.");
        }
    }
}
