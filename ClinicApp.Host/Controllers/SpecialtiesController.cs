using ClinicApp.Application.DTOs.Specialty;
using ClinicApp.Application.Interfaces;
using ClinicApp.Host.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicApp.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtiesController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet( RouteNames.Specialties_GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _specialtyService.GetAllSpecialtiesAsync();
            return response.Success ? Ok(response) : NoContent();
        }

        [HttpGet("GetById{id}" )]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _specialtyService.GetSpecialtyByIdAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateSpecialtyDto specialtyDto)
        {
            // Best practice: Create endpoint should return 201 Created with Location header.
            // Service should return the created resource (or its Id). Adapt the service to return the created DTO.
            var response = await _specialtyService.CreateSpecialtyAsync(specialtyDto);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("Update{id}", Name = RouteNames.Specialties_Update)]
        public async Task<IActionResult> Update(int id, UpdateSpecialtyDto specialtyDto)
        {
            var response = await _specialtyService.UpdateSpecialtyAsync(id, specialtyDto);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("Delete{id}", Name = RouteNames.Specialties_Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _specialtyService.DeleteSpecialtyAsync(id);
            if (!response.Success)
                return NotFound(response);
            return Ok(response);
        }
    }
}