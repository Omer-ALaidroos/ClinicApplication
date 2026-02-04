namespace ClinicApp.Application.DTOs.Doctor
{
    public class GetDoctorDto
    {
        public int DoctorId { get; set; }
        public int UserId { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public bool IsActive { get; set; }
        public int SpecialtyId { get; set; }
        public required string SpecialtyName { get; set; }
        public int ConsultationDuration { get; set; }
    }
}
