using ClinicApp.Application.DTOs.Doctor;
using FluentValidation;

namespace ClinicApp.Application.Validation.Doctor
{
    public class UpdateDoctorValidator : AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                 .NotEmpty().WithMessage("Phone number is required.")
                 .Matches(@"^(77|71|70|73)\d{7}$").WithMessage("Phone number must be 9 digits and start with 77, 71, 70, or 73.");

            RuleFor(x => x.SpecialtyId)
                .GreaterThan(0).WithMessage("Specialty is required.");

            RuleFor(x => x.ConsultationDuration)
                .GreaterThan(0).WithMessage("Consultation duration must be a positive number.");
        }
    }
}
