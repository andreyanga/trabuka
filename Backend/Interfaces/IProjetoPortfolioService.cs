using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IProjetoPortfolioService
    {
        Task<IEnumerable<ProjetoPortfolioReadDto>> GetAllAsync();
        Task<ProjetoPortfolioReadDto> GetByIdAsync(int id);
        Task<ProjetoPortfolioReadDto> CreateAsync(ProjetoPortfolioCreateDto dto);
        Task<ProjetoPortfolioReadDto> UpdateAsync(int id, ProjetoPortfolioUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 