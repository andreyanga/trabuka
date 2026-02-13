using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IExperienciaService
    {
        Task<IEnumerable<ExperienciaReadDto>> GetAllAsync();
        Task<ExperienciaReadDto> GetByIdAsync(int id);
        Task<ExperienciaReadDto> CreateAsync(ExperienciaCreateDto dto);
        Task<ExperienciaReadDto> UpdateAsync(int id, ExperienciaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 