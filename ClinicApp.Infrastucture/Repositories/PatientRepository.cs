using ClinicApp.Application.DTOs.Patient;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Infrastucture.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicAppContext _context;

        public PatientRepository(ClinicAppContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients
                .AsNoTracking()
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return  _context.Patients
                  .AsNoTracking()
                  .Include(p => p.User)
                  .ToList();
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
        }

        public void Update(Patient patient)
        {
            _context.Patients.Update(patient);
        }

        public void Delete(Patient patient)
        {
            _context.Patients.Remove(patient);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
