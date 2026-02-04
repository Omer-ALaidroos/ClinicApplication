namespace ClinicApp.Application.DTOs.Specialty
{
    public class UpdateSpecialtyDto
    {
      
        public required string SpecialtyName { get; set; }
        public required  string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
