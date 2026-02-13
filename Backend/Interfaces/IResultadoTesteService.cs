using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IResultadoTesteService
    {
        Task<IEnumerable<ResultadoTesteReadDto>> GetAllAsync();
        Task<ResultadoTesteReadDto> GetByIdAsync(int id);
        Task<ResultadoTesteReadDto> CreateAsync(ResultadoTesteCreateDto dto);
        Task<ResultadoTesteReadDto> UpdateAsync(int id, ResultadoTesteUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 