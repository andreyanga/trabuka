using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class EducacaoService : IEducacaoService
    {
        private readonly Data.TrabukaDbContext _context;

        public EducacaoService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EducacaoReadDto>> GetAllAsync()
        {
            var educacoes = _context.Educacoes.ToList();
            var dtos = educacoes.Select(e => MapToReadDto(e)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<EducacaoReadDto> GetByIdAsync(int id)
        {
            var educacao = await _context.Educacoes.FindAsync(id);
            return educacao == null ? null : MapToReadDto(educacao);
        }

        public async Task<EducacaoReadDto> CreateAsync(EducacaoCreateDto dto)
        {
            var educacao = new Educacao
            {
                PortfolioId = dto.PortfolioId,
                Curso = dto.Curso,
                Instituicao = dto.Instituicao,
                periodo_inicio = dto.PeriodoInicio,
                periodo_fim = dto.PeriodoFim,
                Descricao = dto.Descricao,
                created_at = System.DateTime.UtcNow
            };
            _context.Educacoes.Add(educacao);
            await _context.SaveChangesAsync();
            return MapToReadDto(educacao);
        }

        public async Task<EducacaoReadDto> UpdateAsync(int id, EducacaoUpdateDto dto)
        {
            var educacao = await _context.Educacoes.FindAsync(id);
            if (educacao == null) return null;
            educacao.Curso = dto.Curso;
            educacao.Instituicao = dto.Instituicao;
            educacao.periodo_inicio = dto.PeriodoInicio;
            educacao.periodo_fim = dto.PeriodoFim;
            educacao.Descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(educacao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var educacao = await _context.Educacoes.FindAsync(id);
            if (educacao == null) return false;
            _context.Educacoes.Remove(educacao);
            await _context.SaveChangesAsync();
            return true;
        }

        private EducacaoReadDto MapToReadDto(Educacao educacao)
        {
            return new EducacaoReadDto
            {
                Id = educacao.id,
                PortfolioId = educacao.PortfolioId,
                Curso = educacao.Curso,
                Instituicao = educacao.Instituicao,
                PeriodoInicio = educacao.periodo_inicio,
                PeriodoFim = educacao.periodo_fim,
                Descricao = educacao.Descricao,
                CreatedAt = educacao.created_at
            };
        }
    }
} 