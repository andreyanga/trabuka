using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface ILogService
    {
        Task<IEnumerable<LogReadDto>> GetAllAsync();
        Task<LogReadDto> GetByIdAsync(int id);
        Task<LogReadDto> CreateAsync(LogCreateDto dto);
        Task<LogReadDto> UpdateAsync(int id, LogUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 