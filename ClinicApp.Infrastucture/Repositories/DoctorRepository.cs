using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Infrastucture.Repositories
{
    public class DoctorRepository(ClinicAppContext context) : IDoctorRepository
    {
        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await context.Doctors
                .AsNoTracking()
                .Include(d => d.User)
                .Include(d => d.Specialty)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await context.Doctors
                  .AsNoTracking()
                  .Include(d => d.User)
                  .Include(d => d.Specialty)
                  .ToListAsync();
        }

        public async Task AddAsync(Doctor doctor)
        {
            await context.Doctors.AddAsync(doctor);
        }

        public void Update(Doctor doctor)
        {
            context.Doctors.Update(doctor);
        }

        public void Delete(Doctor doctor)
        {
            context.Doctors.Remove(doctor);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
