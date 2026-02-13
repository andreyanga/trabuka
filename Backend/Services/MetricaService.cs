using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class MetricaService : IMetricaService
    {
        private readonly Data.TrabukaDbContext _context;

        public MetricaService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetricaReadDto>> GetAllAsync()
        {
            var metricas = _context.Metricas;
            var dtos = new List<MetricaReadDto>();
            foreach (var metrica in metricas)
            {
                dtos.Add(MapToReadDto(metrica));
            }
            return await Task.FromResult(dtos);
        }

        public async Task<MetricaReadDto> GetByIdAsync(int id)
        {
            var metrica = await _context.Metricas.FindAsync(id);
            return metrica == null ? null : MapToReadDto(metrica);
        }

        public async Task<MetricaReadDto> CreateAsync(MetricaCreateDto dto)
        {
            var metrica = new Metrica
            {
                tipo = dto.Tipo,
                valor = dto.Valor,
                data = dto.Data,
                descricao = dto.Descricao
            };
            _context.Metricas.Add(metrica);
            await _context.SaveChangesAsync();
            return MapToReadDto(metrica);
        }

        public async Task<MetricaReadDto> UpdateAsync(int id, MetricaUpdateDto dto)
        {
            var metrica = await _context.Metricas.FindAsync(id);
            if (metrica == null) return null;
            metrica.valor = dto.Valor;
            metrica.descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(metrica);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var metrica = await _context.Metricas.FindAsync(id);
            if (metrica == null) return false;
            _context.Metricas.Remove(metrica);
            await _context.SaveChangesAsync();
            return true;
        }

        private MetricaReadDto MapToReadDto(Metrica metrica)
        {
            return new MetricaReadDto
            {
                Id = metrica.id,
                Tipo = metrica.tipo,
                Valor = metrica.valor,
                Data = metrica.data,
                Descricao = metrica.descricao
            };
        }
    }
} 