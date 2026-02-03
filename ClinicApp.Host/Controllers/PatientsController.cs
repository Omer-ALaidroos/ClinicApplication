using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicApp.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController(IPatientService patientService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPatients()
        {
            var response = await patientService.GetAllPatientsAsync();
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var response = await patientService.GetPatientByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto patientDto)
        {
            var response = await patientService.CreatePatientAsync(patientDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto patientDto)
        {
            var response = await patientService.UpdatePatientAsync(id, patientDto);
            return response.Success ? Ok(response) : BadRequest(response);

        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var response = await patientService.DeletePatientAsync(id);
            return response.Success ? Ok(response) : BadRequest(response);

        }
    }
}
