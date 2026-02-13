using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ServicosRelacionadosService : IServicosRelacionadosService
    {
        private readonly Data.TrabukaDbContext _context;

        public ServicosRelacionadosService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicosRelacionadosReadDto>> GetAllAsync()
        {
            var servicosRelacionados = _context.ServicosRelacionados.ToList();
            var dtos = servicosRelacionados.Select(r => MapToReadDto(r)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<ServicosRelacionadosReadDto> GetByIdAsync(int servicoId, int relacionadoId)
        {
            var relacao = _context.ServicosRelacionados.FirstOrDefault(r => r.servico_id == servicoId && r.relacionado_id == relacionadoId);
            return relacao == null ? null : MapToReadDto(relacao);
        }

        public async Task<ServicosRelacionadosReadDto> CreateAsync(ServicosRelacionadosCreateDto dto)
        {
            var relacao = new ServicosRelacionados
            {
                servico_id = dto.ServicoId,
                relacionado_id = dto.RelacionadoId
            };
            _context.ServicosRelacionados.Add(relacao);
            await _context.SaveChangesAsync();
            return MapToReadDto(relacao);
        }

        public async Task<ServicosRelacionadosReadDto> UpdateAsync(int servicoId, int relacionadoId, ServicosRelacionadosUpdateDto dto)
        {
            var relacao = _context.ServicosRelacionados.FirstOrDefault(r => r.servico_id == servicoId && r.relacionado_id == relacionadoId);
            if (relacao == null) return null;
            relacao.servico_id = dto.ServicoId;
            relacao.relacionado_id = dto.RelacionadoId;
            await _context.SaveChangesAsync();
            return MapToReadDto(relacao);
        }

        public async Task<bool> DeleteAsync(int servicoId, int relacionadoId)
        {
            var relacao = _context.ServicosRelacionados.FirstOrDefault(r => r.servico_id == servicoId && r.relacionado_id == relacionadoId);
            if (relacao == null) return false;
            _context.ServicosRelacionados.Remove(relacao);
            await _context.SaveChangesAsync();
            return true;
        }

        private ServicosRelacionadosReadDto MapToReadDto(ServicosRelacionados relacao)
        {
            var servico = _context.Servicos.FirstOrDefault(s => s.id == relacao.servico_id);
            var relacionado = _context.Servicos.FirstOrDefault(s => s.id == relacao.relacionado_id);
            return new ServicosRelacionadosReadDto
            {
                ServicoId = relacao.servico_id,
                RelacionadoId = relacao.relacionado_id,
                ServicoTitulo = servico?.Titulo,
                RelacionadoTitulo = relacionado?.Titulo
            };
        }
    }
} 