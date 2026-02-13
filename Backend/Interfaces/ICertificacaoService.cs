using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface ICertificacaoService
    {
        Task<IEnumerable<CertificacaoReadDto>> GetAllAsync();
        Task<CertificacaoReadDto> GetByIdAsync(int id);
        Task<CertificacaoReadDto> CreateAsync(CertificacaoCreateDto dto);
        Task<CertificacaoReadDto> UpdateAsync(int id, CertificacaoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 