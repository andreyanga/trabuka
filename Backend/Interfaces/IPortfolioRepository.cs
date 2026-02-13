using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<IEnumerable<Portfolio>> GetAllAsync();
        Task<Portfolio> GetByIdAsync(int id);
        Task AddAsync(Portfolio portfolio);
        void Update(Portfolio portfolio);
        void Delete(Portfolio portfolio);
        Task<bool> SaveChangesAsync();
    }
} 