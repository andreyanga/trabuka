using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IProjetoService
    {
        Task<IEnumerable<ProjetoReadDto>> GetAllAsync();
        Task<ProjetoReadDto> GetByIdAsync(int id);
        Task<ProjetoReadDto> CreateAsync(ProjetoCreateDto dto);
        Task<ProjetoReadDto> UpdateAsync(int id, ProjetoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProjetoReadDto>> GetByEmpresaAsync(int empresaId);
        Task<IEnumerable<ProjetoReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusProjeto status);
        Task<IEnumerable<ProjetoReadDto>> GetByTipoAsync(TrabukaApi.Models.Enums.TipoProjeto tipo);
        Task<IEnumerable<ProjetoReadDto>> GetByLocalizacaoAsync(string provincia);
        Task<IEnumerable<ProjetoReadDto>> GetByFaixaSalarialAsync(decimal minSalario, decimal maxSalario);
        Task<ProjetoReadDto> AtualizarImagemCapaAsync(int id, string caminhoImagem);
    }
} 