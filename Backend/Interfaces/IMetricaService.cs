using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;

namespace TrabukaApi.Interfaces
{
    public interface IMetricaService
    {
        Task<IEnumerable<MetricaReadDto>> GetAllAsync();
        Task<MetricaReadDto> GetByIdAsync(int id);
        Task<MetricaReadDto> CreateAsync(MetricaCreateDto dto);
        Task<MetricaReadDto> UpdateAsync(int id, MetricaUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
} 