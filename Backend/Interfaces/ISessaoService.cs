using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface ISessaoService
    {
        Task<IEnumerable<SessaoReadDto>> GetAllAsync();
        Task<SessaoReadDto> GetByIdAsync(int id);
        Task<SessaoReadDto> CreateAsync(SessaoCreateDto dto);
        Task<SessaoReadDto> UpdateAsync(int id, SessaoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 