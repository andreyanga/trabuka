using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IFAQRepository
    {
        Task<IEnumerable<FAQ>> GetAllAsync();
        Task<FAQ> GetByIdAsync(int id);
        Task AddAsync(FAQ faq);
        void Update(FAQ faq);
        void Delete(FAQ faq);
        Task<bool> SaveChangesAsync();
    }
} 