using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IConfiguracaoService
    {
        Task<IEnumerable<ConfiguracaoReadDto>> GetAllAsync();
        Task<ConfiguracaoReadDto> GetByIdAsync(int id);
        Task<ConfiguracaoReadDto> CreateAsync(ConfiguracaoCreateDto dto);
        Task<ConfiguracaoReadDto> UpdateAsync(int id, ConfiguracaoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 