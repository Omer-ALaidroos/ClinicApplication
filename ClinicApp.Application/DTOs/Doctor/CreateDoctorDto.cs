namespace ClinicApp.Application.DTOs.Doctor
{
    public class CreateDoctorDto
    {
         public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        
        public bool IsActive { get; set; }
        public int SpecialtyId { get; set; }
        public int ConsultationDuration { get; set; }
    }
}
