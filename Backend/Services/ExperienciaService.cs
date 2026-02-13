using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ExperienciaService : IExperienciaService
    {
        private readonly Data.TrabukaDbContext _context;

        public ExperienciaService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExperienciaReadDto>> GetAllAsync()
        {
            var experiencias = _context.Experiencias.ToList();
            var dtos = experiencias.Select(e => MapToReadDto(e)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<ExperienciaReadDto> GetByIdAsync(int id)
        {
            var experiencia = await _context.Experiencias.FindAsync(id);
            return experiencia == null ? null : MapToReadDto(experiencia);
        }

        public async Task<ExperienciaReadDto> CreateAsync(ExperienciaCreateDto dto)
        {
            var experiencia = new Experiencia
            {
                PortfolioId = dto.PortfolioId,
                Cargo = dto.Cargo,
                Empresa = dto.Empresa,
                periodo_inicio = dto.PeriodoInicio,
                periodo_fim = dto.PeriodoFim,
                Conquistas = dto.Conquistas,
                created_at = System.DateTime.UtcNow
            };
            _context.Experiencias.Add(experiencia);
            await _context.SaveChangesAsync();
            return MapToReadDto(experiencia);
        }

        public async Task<ExperienciaReadDto> UpdateAsync(int id, ExperienciaUpdateDto dto)
        {
            var experiencia = await _context.Experiencias.FindAsync(id);
            if (experiencia == null) return null;
            experiencia.Cargo = dto.Cargo;
            experiencia.Empresa = dto.Empresa;
            experiencia.periodo_inicio = dto.PeriodoInicio;
            experiencia.periodo_fim = dto.PeriodoFim;
            experiencia.Conquistas = dto.Conquistas;
            await _context.SaveChangesAsync();
            return MapToReadDto(experiencia);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var experiencia = await _context.Experiencias.FindAsync(id);
            if (experiencia == null) return false;
            _context.Experiencias.Remove(experiencia);
            await _context.SaveChangesAsync();
            return true;
        }

        private ExperienciaReadDto MapToReadDto(Experiencia experiencia)
        {
            return new ExperienciaReadDto
            {
                Id = experiencia.id,
                PortfolioId = experiencia.PortfolioId,
                Cargo = experiencia.Cargo,
                Empresa = experiencia.Empresa,
                PeriodoInicio = experiencia.periodo_inicio,
                PeriodoFim = experiencia.periodo_fim,
                Conquistas = experiencia.Conquistas,
                CreatedAt = experiencia.created_at
            };
        }
    }
} 