using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IEducacaoService
    {
        Task<IEnumerable<EducacaoReadDto>> GetAllAsync();
        Task<EducacaoReadDto> GetByIdAsync(int id);
        Task<EducacaoReadDto> CreateAsync(EducacaoCreateDto dto);
        Task<EducacaoReadDto> UpdateAsync(int id, EducacaoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 