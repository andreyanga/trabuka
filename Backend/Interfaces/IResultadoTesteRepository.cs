using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IResultadoTesteRepository
    {
        Task<IEnumerable<ResultadoTeste>> GetAllAsync();
        Task<ResultadoTeste> GetByIdAsync(int id);
        Task AddAsync(ResultadoTeste resultadoTeste);
        void Update(ResultadoTeste resultadoTeste);
        void Delete(ResultadoTeste resultadoTeste);
        Task<bool> SaveChangesAsync();
    }
} 