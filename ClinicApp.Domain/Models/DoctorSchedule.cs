using System;
using System.Collections.Generic;

namespace ClinicApp.Domain.Models;

public partial class DoctorSchedule
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public byte DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsActive { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
