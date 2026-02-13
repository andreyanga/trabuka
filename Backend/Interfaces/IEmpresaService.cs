using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaReadDto>> GetAllAsync();
        Task<EmpresaReadDto> GetByIdAsync(int id);
        Task<EmpresaReadDto> CreateAsync(EmpresaCreateDto dto);
        Task<EmpresaReadDto> UpdateAsync(int id, EmpresaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EmpresaReadDto>> GetBySetorAsync(TrabukaApi.Models.Enums.SetorEmpresa setor);
        Task<IEnumerable<EmpresaReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusEmpresa status);
        Task<IEnumerable<EmpresaReadDto>> GetByLocalizacaoAsync(string provincia);
    }
} 