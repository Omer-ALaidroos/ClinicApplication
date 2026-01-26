namespace ClinicApp.Application.DTOs.Specialty
{
    public class CreateSpecialtyDto
    {
        public required string SpecialtyName { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
