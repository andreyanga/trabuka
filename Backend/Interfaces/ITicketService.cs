using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketReadDto>> GetAllAsync();
        Task<TicketReadDto> GetByIdAsync(int id);
        Task<TicketReadDto> CreateAsync(TicketCreateDto dto);
        Task<TicketReadDto> UpdateAsync(int id, TicketUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TicketReadDto>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<TicketReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusTicket status);
        Task<IEnumerable<TicketReadDto>> GetByCategoriaAsync(TrabukaApi.Models.Enums.CategoriaTicket categoria);
        Task<IEnumerable<TicketReadDto>> GetByPrioridadeAsync(TrabukaApi.Models.Enums.PrioridadeTicket prioridade);
        Task<TicketReadDto> UpdateStatusAsync(int id, TrabukaApi.Models.Enums.StatusTicket novoStatus);
    }
} 