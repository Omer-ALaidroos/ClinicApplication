using System;

namespace ClinicApp.Application.DTOs.MedicalRecord
{
    public class CreateMedicalRecordDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
         public int? AppointmentId { get; set; }
        public DateTime VisitDate { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public string? Notes { get; set; }
    }
}
