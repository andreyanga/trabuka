using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IPortfolioService
    {
        Task<IEnumerable<PortfolioReadDto>> GetAllAsync();
        Task<PortfolioReadDto> GetByIdAsync(int id);
        Task<PortfolioReadDto> CreateAsync(PortfolioCreateDto dto);
        Task<PortfolioReadDto> UpdateAsync(int id, PortfolioUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PortfolioReadDto>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<PortfolioReadDto>> GetByCategoriaAsync(TrabukaApi.Models.Enums.CategoriaPortfolio categoria);
        Task<IEnumerable<PortfolioReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusPortfolio status);
        Task<IEnumerable<PortfolioReadDto>> GetByTecnologiaAsync(string tecnologia);
        Task<PortfolioReadDto> UpdateStatusAsync(int id, TrabukaApi.Models.Enums.StatusPortfolio novoStatus);
        Task<PortfolioReadDto> AtualizarImagemAsync(int id, string caminhoImagem, int posicao);
    }
} 