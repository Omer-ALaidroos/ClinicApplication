using ClinicApp.Application.DTOs.User;
using ClinicApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicApp.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUsers([FromQuery] string? role)
        {
            if (!string.IsNullOrEmpty(role))
            {
                var response = await userService.GetUsersByRoleAsync(role);
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
            else
            {
                var response = await userService.GetAllUsersAsync();
                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Data);
            }
        }

        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await userService.GetUserByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            var response = await userService.CreateUserAsync(userDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto userDto)
        {
            var response = await userService.UpdateUserAsync(id, userDto);
            return response.Success ? NoContent() : NotFound(response.Message);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await userService.DeleteUserAsync(id);
            return response.Success ? NoContent() : NotFound(response.Message);
            
        }
    }
}
