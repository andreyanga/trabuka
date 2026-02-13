using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IFuncionalidadeService
    {
        Task<IEnumerable<FuncionalidadeReadDto>> GetAllAsync();
        Task<FuncionalidadeReadDto> GetByIdAsync(int id);
        Task<FuncionalidadeReadDto> CreateAsync(FuncionalidadeCreateDto dto);
        Task<FuncionalidadeReadDto> UpdateAsync(int id, FuncionalidadeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 