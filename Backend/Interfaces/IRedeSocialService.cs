using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IRedeSocialService
    {
        Task<IEnumerable<RedeSocialReadDto>> GetAllAsync();
        Task<RedeSocialReadDto> GetByIdAsync(int id);
        Task<RedeSocialReadDto> CreateAsync(RedeSocialCreateDto dto);
        Task<RedeSocialReadDto> UpdateAsync(int id, RedeSocialUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 