using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface ITesteService
    {
        Task<IEnumerable<TesteReadDto>> GetAllAsync();
        Task<TesteReadDto> GetByIdAsync(int id);
        Task<TesteReadDto> CreateAsync(TesteCreateDto dto);
        Task<TesteReadDto> UpdateAsync(int id, TesteUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        
        // Métodos para quiz
        Task<TesteComQuestoesDto> IniciarTesteAsync(int testeId, int usuarioId);
        Task<ResultadoTesteCompletoDto> SubmeterTesteAsync(SubmeterTesteDto dto);
        Task<IEnumerable<TesteReadDto>> GetTestesDisponiveisAsync(int usuarioId);
    }
} 