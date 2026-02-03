using System;
using System.Collections.Generic;

namespace ClinicApp.Domain.Models;

public partial class Appointment
{
    public int Id { get; private set; }

    public int DoctorId { get; private set; }

    public int PatientId { get; private set; }

    public DateTime StartDateTime { get; private set; }

    public DateTime EndDateTime { get; private set; }

    public string Status { get; private set; }

    public string? Notes { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public virtual Doctor Doctor { get; private set; }

    public virtual ICollection<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; private set; }

    private Appointment() 
    {
        // Required by EF Core
        Status = null!;
        Doctor = null!;
        Patient = null!;
    }

    public Appointment(int doctorId, int patientId, DateTime startDateTime, DateTime endDateTime, string? notes)
    {
        if (startDateTime < DateTime.UtcNow)
            throw new ArgumentException("Appointment start date cannot be in the past.");

        if (startDateTime >= endDateTime)
            throw new ArgumentException("Appointment end time must be after the start time.");

        DoctorId = doctorId;
        PatientId = patientId;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        Status = "Scheduled"; // Default status
        Notes = notes;
        CreatedAt = DateTime.UtcNow;
    }

    public void Cancel(string reason)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("A reason must be provided for cancellation.");

        if (Status == "Completed" || Status == "Cancelled")
            throw new InvalidOperationException("Cannot cancel a completed or already cancelled appointment.");

        Status = "Cancelled";
        Notes = $"Cancelled with reason: {reason}. Original Notes: {Notes}";
    }

    public void Reschedule(DateTime newStart, DateTime newEnd)
    {
        if (newStart < DateTime.UtcNow)
            throw new ArgumentException("New start date cannot be in the past.");

        if (newStart >= newEnd)
            throw new ArgumentException("New end time must be after the new start time.");
        
        if (Status == "Cancelled")
            throw new InvalidOperationException("Cannot reschedule a cancelled appointment.");

        StartDateTime = newStart;
        EndDateTime = newEnd;
        Status = "Rescheduled";
    }

    public void Complete()
    {
        if (Status != "Confirmed" && Status != "Scheduled" && Status != "Rescheduled")
            throw new InvalidOperationException("Only a confirmed, scheduled, or rescheduled appointment can be marked as completed.");

        Status = "Completed";
    }

    public void Confirm()
    {
        if(Status != "Scheduled")
            throw new InvalidOperationException("Only a scheduled appointment can be confirmed.");

        Status = "Confirmed";
    }
}