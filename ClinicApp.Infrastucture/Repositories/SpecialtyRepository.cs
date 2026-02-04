using ClinicApp.Domain.Interfaces;
using ClinicApp.Domain.Models;
using ClinicApp.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ClinicApp.Infrastucture.Repositories
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly ClinicAppContext _context;

        public SpecialtyRepository(ClinicAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Specialty>> GetAllAsync()
        {
            return await _context.Specialties.AsNoTracking().ToListAsync();
        }

        public async Task<Specialty> GetByIdAsync(int id)
        {
            return await _context.Specialties.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Specialty specialty)
        {
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialty specialty)
        {
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty != null)
            {
                _context.Specialties.Remove(specialty);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsInUseAsync(int specialtyId)
        {
            
            return await _context.Doctors.AnyAsync(d => d.SpecialtyId == specialtyId);
        }
    }
}
