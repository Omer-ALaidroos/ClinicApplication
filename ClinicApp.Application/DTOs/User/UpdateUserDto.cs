namespace ClinicApp.Application.DTOs.User
{
    public class UpdateUserDto
    {
      
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
       
        
        public bool IsActive { get; set; }
    }
}
