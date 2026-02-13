using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IMentoriaService
    {
        Task<IEnumerable<MentoriaReadDto>> GetAllAsync();
        Task<MentoriaReadDto> GetByIdAsync(int id);
        Task<MentoriaReadDto> CreateAsync(MentoriaCreateDto dto);
        Task<MentoriaReadDto> UpdateAsync(int id, MentoriaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 