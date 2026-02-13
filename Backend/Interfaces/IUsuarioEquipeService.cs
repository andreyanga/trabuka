using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IUsuarioEquipeService
    {
        Task<IEnumerable<UsuarioEquipeReadDto>> GetAllAsync();
        Task<UsuarioEquipeReadDto> GetByIdAsync(int usuarioId, int equipeId);
        Task<UsuarioEquipeReadDto> CreateAsync(UsuarioEquipeCreateDto dto);
        Task<UsuarioEquipeReadDto> UpdateAsync(int usuarioId, int equipeId, UsuarioEquipeUpdateDto dto);
        Task<bool> DeleteAsync(int usuarioId, int equipeId);
    }
} 