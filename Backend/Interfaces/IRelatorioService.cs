using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IRelatorioService
    {
        Task<IEnumerable<RelatorioReadDto>> GetAllAsync();
        Task<RelatorioReadDto> GetByIdAsync(int id);
        Task<RelatorioReadDto> CreateAsync(RelatorioCreateDto dto);
        Task<RelatorioReadDto> UpdateAsync(int id, RelatorioUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 