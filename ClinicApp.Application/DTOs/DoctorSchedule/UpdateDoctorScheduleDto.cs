using System;

namespace ClinicApp.Application.DTOs.DoctorSchedule
{
    public class UpdateDoctorScheduleDto
    {
       
       
        public byte DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
