using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IPagamentoService
    {
        Task<IEnumerable<PagamentoReadDto>> GetAllAsync();
        Task<PagamentoReadDto> GetByIdAsync(int id);
        Task<PagamentoReadDto> CreateAsync(PagamentoCreateDto dto);
        Task<PagamentoReadDto> UpdateAsync(int id, PagamentoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PagamentoReadDto>> GetByUsuarioAsync(int usuarioId);
        Task<IEnumerable<PagamentoReadDto>> GetByEmpresaAsync(int empresaId);
        Task<IEnumerable<PagamentoReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusPagamento status);
        Task<IEnumerable<PagamentoReadDto>> GetByTipoAsync(TrabukaApi.Models.Enums.TipoPagamento tipo);
        Task<IEnumerable<PagamentoReadDto>> GetByPeriodoAsync(System.DateTime dataInicio, System.DateTime dataFim);
    }
} 