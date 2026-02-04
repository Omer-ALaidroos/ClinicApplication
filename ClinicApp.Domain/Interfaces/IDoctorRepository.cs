using ClinicApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task AddAsync(Doctor doctor);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        Task<int> SaveChangesAsync();
    }
}
