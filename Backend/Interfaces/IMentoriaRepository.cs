using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IMentoriaRepository
    {
        Task<IEnumerable<Mentoria>> GetAllAsync();
        Task<Mentoria> GetByIdAsync(int id);
        Task AddAsync(Mentoria mentoria);
        void Update(Mentoria mentoria);
        void Delete(Mentoria mentoria);
        Task<bool> SaveChangesAsync();
    }
} 