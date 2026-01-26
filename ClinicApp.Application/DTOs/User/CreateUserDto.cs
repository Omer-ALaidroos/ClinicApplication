namespace ClinicApp.Application.DTOs.User
{
    public class CreateUserDto
    {
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public  required string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
