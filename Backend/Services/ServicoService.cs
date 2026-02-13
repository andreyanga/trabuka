using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ServicoService : IServicoService
    {
        private readonly Data.TrabukaDbContext _context;

        public ServicoService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicoReadDto>> GetAllAsync()
        {
            var servicos = _context.Servicos;
            var dtos = new List<ServicoReadDto>();
            foreach (var servico in servicos)
            {
                dtos.Add(MapToReadDto(servico));
            }
            return await Task.FromResult(dtos);
        }

        public async Task<ServicoReadDto> GetByIdAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            return servico == null ? null : MapToReadDto(servico);
        }

        public async Task<ServicoReadDto> CreateAsync(ServicoCreateDto dto)
        {
            var servico = new Servico
            {
                PortfolioId = dto.PortfolioId,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao
            };
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
            return MapToReadDto(servico);
        }

        public async Task<ServicoReadDto> UpdateAsync(int id, ServicoUpdateDto dto)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return null;
            servico.Titulo = dto.Titulo;
            servico.Descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(servico);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null) return false;
            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();
            return true;
        }

        private ServicoReadDto MapToReadDto(Servico servico)
        {
            return new ServicoReadDto
            {
                Id = servico.id,
                PortfolioId = servico.PortfolioId,
                Titulo = servico.Titulo,
                Descricao = servico.Descricao
            };
        }
    }
} 