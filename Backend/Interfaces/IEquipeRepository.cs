using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IEquipeRepository
    {
        Task<IEnumerable<Equipe>> GetAllAsync();
        Task<Equipe> GetByIdAsync(int id);
        Task AddAsync(Equipe equipe);
        void Update(Equipe equipe);
        void Delete(Equipe equipe);
        Task<bool> SaveChangesAsync();
    }
} 