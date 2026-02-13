using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class CertificacaoService : ICertificacaoService
    {
        private readonly Data.TrabukaDbContext _context;

        public CertificacaoService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CertificacaoReadDto>> GetAllAsync()
        {
            var certificacoes = _context.Certificacoes.ToList();
            var dtos = certificacoes.Select(c => MapToReadDto(c)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<CertificacaoReadDto> GetByIdAsync(int id)
        {
            var certificacao = await _context.Certificacoes.FindAsync(id);
            return certificacao == null ? null : MapToReadDto(certificacao);
        }

        public async Task<CertificacaoReadDto> CreateAsync(CertificacaoCreateDto dto)
        {
            var certificacao = new Certificacao
            {
                PortfolioId = dto.PortfolioId,
                Nome = dto.Nome,
                Ano = dto.Ano,
                created_at = System.DateTime.UtcNow
            };
            _context.Certificacoes.Add(certificacao);
            await _context.SaveChangesAsync();
            return MapToReadDto(certificacao);
        }

        public async Task<CertificacaoReadDto> UpdateAsync(int id, CertificacaoUpdateDto dto)
        {
            var certificacao = await _context.Certificacoes.FindAsync(id);
            if (certificacao == null) return null;
            certificacao.Nome = dto.Nome;
            certificacao.Ano = dto.Ano;
            await _context.SaveChangesAsync();
            return MapToReadDto(certificacao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var certificacao = await _context.Certificacoes.FindAsync(id);
            if (certificacao == null) return false;
            _context.Certificacoes.Remove(certificacao);
            await _context.SaveChangesAsync();
            return true;
        }

        private CertificacaoReadDto MapToReadDto(Certificacao certificacao)
        {
            return new CertificacaoReadDto
            {
                Id = certificacao.id,
                PortfolioId = certificacao.PortfolioId,
                Nome = certificacao.Nome,
                Ano = certificacao.Ano,
                CreatedAt = certificacao.created_at
            };
        }
    }
} 