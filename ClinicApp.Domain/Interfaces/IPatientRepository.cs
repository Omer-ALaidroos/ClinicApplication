using ClinicApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicApp.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient patient);
        void Update(Patient patient);
        void Delete(Patient patient);
        Task<int> SaveChangesAsync();
    }
}
