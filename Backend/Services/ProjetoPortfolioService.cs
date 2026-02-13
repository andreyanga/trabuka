using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class ProjetoPortfolioService : IProjetoPortfolioService
    {
        private readonly Data.TrabukaDbContext _context;

        public ProjetoPortfolioService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjetoPortfolioReadDto>> GetAllAsync()
        {
            var projetosPortfolio = _context.ProjetosPortfolio.ToList();
            var dtos = projetosPortfolio.Select(p => MapToReadDto(p)).ToList();
            return await Task.FromResult(dtos);
        }

        public async Task<ProjetoPortfolioReadDto> GetByIdAsync(int id)
        {
            var projetoPortfolio = await _context.ProjetosPortfolio.FindAsync(id);
            return projetoPortfolio == null ? null : MapToReadDto(projetoPortfolio);
        }

        public async Task<ProjetoPortfolioReadDto> CreateAsync(ProjetoPortfolioCreateDto dto)
        {
            var projetoPortfolio = new ProjetoPortfolio
            {
                PortfolioId = dto.PortfolioId,
                Categoria = dto.Categoria,
                Titulo = dto.Titulo,
                Cliente = dto.Cliente,
                data_projeto = dto.DataProjeto,
                url_projeto = dto.UrlProjeto,
                Descricao = dto.Descricao,
                created_at = System.DateTime.UtcNow
            };
            _context.ProjetosPortfolio.Add(projetoPortfolio);
            await _context.SaveChangesAsync();
            return MapToReadDto(projetoPortfolio);
        }

        public async Task<ProjetoPortfolioReadDto> UpdateAsync(int id, ProjetoPortfolioUpdateDto dto)
        {
            var projetoPortfolio = await _context.ProjetosPortfolio.FindAsync(id);
            if (projetoPortfolio == null) return null;
            projetoPortfolio.Categoria = dto.Categoria;
            projetoPortfolio.Titulo = dto.Titulo;
            projetoPortfolio.Cliente = dto.Cliente;
            projetoPortfolio.data_projeto = dto.DataProjeto;
            projetoPortfolio.url_projeto = dto.UrlProjeto;
            projetoPortfolio.Descricao = dto.Descricao;
            await _context.SaveChangesAsync();
            return MapToReadDto(projetoPortfolio);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var projetoPortfolio = await _context.ProjetosPortfolio.FindAsync(id);
            if (projetoPortfolio == null) return false;
            _context.ProjetosPortfolio.Remove(projetoPortfolio);
            await _context.SaveChangesAsync();
            return true;
        }

        private ProjetoPortfolioReadDto MapToReadDto(ProjetoPortfolio projeto)
        {
            return new ProjetoPortfolioReadDto
            {
                Id = projeto.id,
                PortfolioId = projeto.PortfolioId,
                Categoria = projeto.Categoria,
                Titulo = projeto.Titulo,
                Cliente = projeto.Cliente,
                DataProjeto = projeto.data_projeto,
                UrlProjeto = projeto.url_projeto,
                Descricao = projeto.Descricao,
                CreatedAt = projeto.created_at
            };
        }
    }
} 