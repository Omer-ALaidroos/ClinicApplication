using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.DTOs.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Application.Validation.Patient
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientValidator()
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

           

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 6 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[^\w]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
