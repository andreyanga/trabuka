using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IFAQService
    {
        Task<IEnumerable<FAQReadDto>> GetAllAsync();
        Task<FAQReadDto> GetByIdAsync(int id);
        Task<FAQReadDto> CreateAsync(FAQCreateDto dto);
        Task<FAQReadDto> UpdateAsync(int id, FAQUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 