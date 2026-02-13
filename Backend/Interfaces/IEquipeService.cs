using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IEquipeService
    {
        Task<IEnumerable<EquipeReadDto>> GetAllAsync();
        Task<EquipeReadDto> GetByIdAsync(int id);
        Task<EquipeReadDto> CreateAsync(EquipeCreateDto dto);
        Task<EquipeReadDto> UpdateAsync(int id, EquipeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 