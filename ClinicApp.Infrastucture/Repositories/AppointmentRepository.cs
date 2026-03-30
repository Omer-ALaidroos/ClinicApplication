using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ClinicApp.Infrastucture.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicAppContext _context;

        public AppointmentRepository(ClinicAppContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        // New method: return appointments for a doctor on the given calendar date
        public async Task<IEnumerable<Appointment>> GetAppointmentsForDoctorByDateAsync(int doctorId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.StartDateTime >= startOfDay && a.StartDateTime < endOfDay)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        // Execute stored procedure and map results to AvailableAppointmentSlot
        public async Task<IEnumerable<AvailableAppointmentSlot>> GetDoctorAvailableSlotsWeeklyAsync(int doctorId, DateTime weekStart)
        {
            var results = new List<AvailableAppointmentSlot>();

            var connection = _context.Database.GetDbConnection();
            await using (connection)
            {
                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                await using var command = connection.CreateCommand();
                command.CommandText = "sp_GetDoctorAvailableAppointmentsWeekly";
                command.CommandType = CommandType.StoredProcedure;

                var doctorParam = new SqlParameter("@DoctorId", SqlDbType.Int) { Value = doctorId };
                var weekStartParam = new SqlParameter("@WeekStart", SqlDbType.Date) { Value = weekStart.Date };

                command.Parameters.Add(doctorParam);
                command.Parameters.Add(weekStartParam);

                await using var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                while (await reader.ReadAsync())
                {
                    var slot = new AvailableAppointmentSlot
                    {
                        WorkDate = reader.GetFieldValue<DateTime>(reader.GetOrdinal("WorkDate")),
                        AvailableFrom = reader.GetFieldValue<DateTime>(reader.GetOrdinal("AvailableFrom")),
                        AvailableTo = reader.GetFieldValue<DateTime>(reader.GetOrdinal("AvailableTo"))
                    };

                    results.Add(slot);
                }
            }

            return results;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return appointment;
        }

        public void Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}