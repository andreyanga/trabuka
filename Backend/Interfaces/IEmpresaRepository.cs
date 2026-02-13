using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa> GetByIdAsync(int id);
        Task AddAsync(Empresa empresa);
        void Update(Empresa empresa);
        void Delete(Empresa empresa);
        Task<bool> SaveChangesAsync();
    }
} 