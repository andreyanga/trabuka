using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IPagamentoRepository
    {
        Task<IEnumerable<Pagamento>> GetAllAsync();
        Task<Pagamento> GetByIdAsync(int id);
        Task AddAsync(Pagamento pagamento);
        void Update(Pagamento pagamento);
        void Delete(Pagamento pagamento);
        Task<bool> SaveChangesAsync();
    }
} 