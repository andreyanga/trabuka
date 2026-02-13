using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface INotificacaoRepository
    {
        Task<IEnumerable<Notificacao>> GetAllAsync();
        Task<Notificacao> GetByIdAsync(int id);
        Task AddAsync(Notificacao notificacao);
        void Update(Notificacao notificacao);
        void Delete(Notificacao notificacao);
        Task<bool> SaveChangesAsync();
    }
} 