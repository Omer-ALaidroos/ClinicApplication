using System;

namespace ClinicApp.Application.DTOs.Patient
{
    public class CreatePatientDto
    {
       public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
        public DateOnly? BirthDate { get; set; }
        public required string Gender { get; set; }
        public required string Address { get; set; }
    }
}
