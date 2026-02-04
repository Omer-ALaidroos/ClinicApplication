using ClinicApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Domain.Interfaces
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> GetAllAsync();
        Task<Specialty> GetByIdAsync(int id);
        Task AddAsync(Specialty specialty);
        Task UpdateAsync(Specialty specialty);
        Task DeleteAsync(int id);

        // New: returns true if any Doctor references this specialty
        Task<bool> IsInUseAsync(int specialtyId);
    }
}
