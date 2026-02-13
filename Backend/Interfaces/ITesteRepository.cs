using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface ITesteRepository
    {
        Task<IEnumerable<Teste>> GetAllAsync();
        Task<Teste> GetByIdAsync(int id);
        Task AddAsync(Teste teste);
        void Update(Teste teste);
        void Delete(Teste teste);
        Task<bool> SaveChangesAsync();
    }
} 