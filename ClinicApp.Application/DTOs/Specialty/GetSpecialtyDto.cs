namespace ClinicApp.Application.DTOs.Specialty
{
    public class GetSpecialtyDto
    {
        public int Id { get; set; }
        public required string SpecialtyName { get; set; }
        public required string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
