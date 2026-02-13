using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Models;

namespace TrabukaApi.Interfaces
{
    public interface IUsuarioEquipeRepository
    {
        Task<IEnumerable<UsuarioEquipe>> GetAllAsync();
        Task<UsuarioEquipe> GetByIdsAsync(int usuarioId, int equipeId);
        Task AddAsync(UsuarioEquipe usuarioEquipe);
        void Update(UsuarioEquipe usuarioEquipe);
        void Delete(UsuarioEquipe usuarioEquipe);
        Task<bool> SaveChangesAsync();
    }
} 