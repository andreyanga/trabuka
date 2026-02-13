using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioReadDto>> GetAllAsync();
        Task<UsuarioReadDto> GetByIdAsync(int id);
        Task<UsuarioReadDto> CreateAsync(UsuarioCreateDto dto);
        Task<UsuarioReadDto> UpdateAsync(int id, UsuarioUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<UsuarioReadDto>> GetByTipoAsync(TrabukaApi.Models.Enums.TipoUsuario tipo);
        Task<IEnumerable<UsuarioReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusUsuario status);
        Task<UsuarioReadDto> AtualizarFotoPerfilAsync(int id, string caminhoFoto);
    }
} 