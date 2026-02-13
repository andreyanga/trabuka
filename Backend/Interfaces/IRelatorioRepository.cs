using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<Relatorio>> GetAllAsync();
        Task<Relatorio> GetByIdAsync(int id);
        Task AddAsync(Relatorio relatorio);
        void Update(Relatorio relatorio);
        void Delete(Relatorio relatorio);
        Task<bool> SaveChangesAsync();
    }
} 