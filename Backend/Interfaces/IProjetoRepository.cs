using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetAllAsync();
        Task<Projeto> GetByIdAsync(int id);
        Task AddAsync(Projeto projeto);
        void Update(Projeto projeto);
        void Delete(Projeto projeto);
        Task<bool> SaveChangesAsync();
    }
} 