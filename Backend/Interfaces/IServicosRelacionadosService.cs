using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IServicosRelacionadosService
    {
        Task<IEnumerable<ServicosRelacionadosReadDto>> GetAllAsync();
        Task<ServicosRelacionadosReadDto> GetByIdAsync(int servicoId, int relacionadoId);
        Task<ServicosRelacionadosReadDto> CreateAsync(ServicosRelacionadosCreateDto dto);
        Task<ServicosRelacionadosReadDto> UpdateAsync(int servicoId, int relacionadoId, ServicosRelacionadosUpdateDto dto);
        Task<bool> DeleteAsync(int servicoId, int relacionadoId);
    }
} 