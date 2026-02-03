using ClinicApp.Application.DTOs.Appointment;
using ClinicApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(int patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patientId);
            return Ok(appointments);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctorId(int doctorId)
        {
            var appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto appointmentDto)
        {
            try
            {
                var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointmentDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = createdAppointment.Id }, createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDto appointmentDto)
        {
            if (id != appointmentDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _appointmentService.UpdateAppointmentAsync(id, appointmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            try
            {
                await _appointmentService.CancelAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
