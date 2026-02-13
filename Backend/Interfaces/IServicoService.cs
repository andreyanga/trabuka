using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<ServicoReadDto>> GetAllAsync();
        Task<ServicoReadDto> GetByIdAsync(int id);
        Task<ServicoReadDto> CreateAsync(ServicoCreateDto dto);
        Task<ServicoReadDto> UpdateAsync(int id, ServicoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 