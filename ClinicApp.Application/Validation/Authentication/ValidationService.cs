using ClinicApp.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicApp.Application.Validation.Authentication
{
    public class ValidationsService : IValidationsService
    {
        public async Task<ServiceResponse> ValidateAsync<T>(T model, IValidator<T> validator)
        {

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                string ErrorToString = string.Join("; ", errors);

                return new ServiceResponse
                (false, ErrorToString);
                 
            }

            return new ServiceResponse (true,null);
        }
    }
}
