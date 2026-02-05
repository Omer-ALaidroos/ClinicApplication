using ClinicApp.Application.DTOs.Patient;
using FluentValidation;

namespace ClinicApp.Application.Validation.Patient
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientValidator()
        {
            RuleFor(x => x.Phone)
                 .NotEmpty().WithMessage("Phone number is required.")
                 .Matches(@"^(77|71|70|73)\d{7}$").WithMessage("Phone number must be 9 digits and start with 77, 71, 70, or 73.");

            RuleFor(x => x.BirthDate)
                 .NotEmpty().WithMessage("Birth date is required.");

            RuleFor(x => x.Gender)
                 .NotEmpty().WithMessage("Gender is required")
                 .Must(x => x == "Male" || x == "Female")
                 .WithMessage("Gender must be either Male or Female.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
