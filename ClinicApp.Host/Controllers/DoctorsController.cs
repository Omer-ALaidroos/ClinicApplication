using ClinicApp.Application.DTOs.Doctor;
using ClinicApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicApp.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var response = await _doctorService.GetAllDoctorsAsync();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            var response = await _doctorService.GetDoctorByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto doctorDto)
        {
            var response = await _doctorService.CreateDoctorAsync(doctorDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDto doctorDto)
        {
            var response = await _doctorService.UpdateDoctorAsync(id, doctorDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var response = await _doctorService.DeleteDoctorAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
