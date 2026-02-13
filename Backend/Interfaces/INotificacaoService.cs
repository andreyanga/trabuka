using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface INotificacaoService
    {
        Task<IEnumerable<NotificacaoReadDto>> GetAllAsync();
        Task<IEnumerable<NotificacaoReadDto>> GetByUsuarioAsync(int usuarioId);
        Task<NotificacaoReadDto> GetByIdAsync(int id);
        Task<NotificacaoReadDto> CreateAsync(NotificacaoCreateDto dto);
        Task<NotificacaoReadDto> UpdateAsync(int id, NotificacaoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 