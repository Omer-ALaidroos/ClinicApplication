using System;
using System.Collections.Generic;

namespace ClinicApp.Infrastucture.Models;

public partial class Specialty
{
    public int Id { get; set; }

    public string SpecialtyName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
