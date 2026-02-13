using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IQuestaoRepository
    {
        Task<IEnumerable<Questao>> GetByTesteIdAsync(int testeId);
        Task<Questao> GetByIdAsync(int id);
        Task AddAsync(Questao questao);
        void Update(Questao questao);
        void Delete(Questao questao);
        Task<bool> SaveChangesAsync();
    }
}
