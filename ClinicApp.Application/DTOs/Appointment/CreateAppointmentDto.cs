using System;
using System.Text.Json.Serialization;

namespace ClinicApp.Application.DTOs.Appointment
{
    public class CreateAppointmentDto
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        
      [JsonConverter(typeof(JsonStringEnumConverter))]
        public required AppointmentStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}
