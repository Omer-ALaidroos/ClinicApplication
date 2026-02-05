using ClinicApp.Application.DTOs.User;
using FluentValidation;

namespace ClinicApp.Application.Validation.Authentication
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
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

          


        }
    }
}
