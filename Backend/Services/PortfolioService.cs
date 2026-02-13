using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<IEnumerable<PortfolioReadDto>> GetAllAsync()
        {
            var portfolios = await _portfolioRepository.GetAllAsync();
            return portfolios.Select(MapToReadDto);
        }

        public async Task<PortfolioReadDto> GetByIdAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
                throw new ArgumentException("Portfólio não encontrado");

            return MapToReadDto(portfolio);
        }

        public async Task<PortfolioReadDto> CreateAsync(PortfolioCreateDto dto)
        {
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(dto.URL))
                throw new ArgumentException("URL é obrigatória");

            var portfolio = new Portfolio
            {
                UsuarioId = dto.UsuarioId,
                URL = dto.URL,
                DataConclusao = dto.DataConclusao,
                Imagem1 = dto.Imagem1 ?? "",
                Imagem2 = dto.Imagem2 ?? ""
            };

            await _portfolioRepository.AddAsync(portfolio);
            await _portfolioRepository.SaveChangesAsync();

            return MapToReadDto(portfolio);
        }

        public async Task<PortfolioReadDto> UpdateAsync(int id, PortfolioUpdateDto dto)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
                throw new ArgumentException("Portfólio não encontrado");

            // Atualizar apenas campos permitidos
            portfolio.URL = dto.URL ?? portfolio.URL;
            portfolio.DataConclusao = dto.DataConclusao;
            portfolio.Imagem1 = dto.Imagem1 ?? portfolio.Imagem1;
            portfolio.Imagem2 = dto.Imagem2 ?? portfolio.Imagem2;

            _portfolioRepository.Update(portfolio);
            await _portfolioRepository.SaveChangesAsync();

            return MapToReadDto(portfolio);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
                return false;

            _portfolioRepository.Delete(portfolio);
            return await _portfolioRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PortfolioReadDto>> GetByUsuarioAsync(int usuarioId)
        {
            var portfolios = await _portfolioRepository.GetAllAsync();
            var portfoliosFiltrados = portfolios.Where(p => p.UsuarioId == usuarioId);
            return portfoliosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PortfolioReadDto>> GetByCategoriaAsync(TrabukaApi.Models.Enums.CategoriaPortfolio categoria)
        {
            // Categoria não existe no modelo Portfolio, retornando todos os portfolios
            var portfolios = await _portfolioRepository.GetAllAsync();
            return portfolios.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PortfolioReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusPortfolio status)
        {
            // Status não existe no modelo Portfolio, retornando todos os portfolios
            var portfolios = await _portfolioRepository.GetAllAsync();
            return portfolios.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PortfolioReadDto>> GetByTecnologiaAsync(string tecnologia)
        {
            // Tecnologias não existe no modelo Portfolio, retornando todos os portfolios
            var portfolios = await _portfolioRepository.GetAllAsync();
            return portfolios.Select(MapToReadDto);
        }

        public async Task<PortfolioReadDto> UpdateStatusAsync(int id, TrabukaApi.Models.Enums.StatusPortfolio novoStatus)
        {
            // Status não existe no modelo Portfolio, retornando o portfolio sem alterações
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null)
                throw new ArgumentException("Portfólio não encontrado");

            return MapToReadDto(portfolio);
        }

        public async Task<PortfolioReadDto> AtualizarImagemAsync(int id, string caminhoImagem, int posicao)
        {
            var portfolio = await _portfolioRepository.GetByIdAsync(id);
            if (portfolio == null) return null;
            if (posicao == 1) portfolio.Imagem1 = caminhoImagem;
            else if (posicao == 2) portfolio.Imagem2 = caminhoImagem;
            await _portfolioRepository.SaveChangesAsync();
            return MapToReadDto(portfolio);
        }

        private static PortfolioReadDto MapToReadDto(Portfolio portfolio)
        {
            // Corrigir caminhos das imagens
            var imagem1 = portfolio.Imagem1;
            var imagem2 = portfolio.Imagem2;

            if (!string.IsNullOrEmpty(imagem1) && imagem1.Contains("src/"))
            {
                imagem1 = Path.GetFileName(imagem1);
            }
            
            if (!string.IsNullOrEmpty(imagem1))
            {
                imagem1 = $"/assets/images/portfolios/{imagem1}";
            }

            if (!string.IsNullOrEmpty(imagem2) && imagem2.Contains("src/"))
            {
                imagem2 = Path.GetFileName(imagem2);
            }
            
            if (!string.IsNullOrEmpty(imagem2))
            {
                imagem2 = $"/assets/images/portfolios/{imagem2}";
            }

            return new PortfolioReadDto
            {
                Id = portfolio.PortfolioId,
                PortfolioId = portfolio.PortfolioId,
                UsuarioId = portfolio.UsuarioId,
                URL = portfolio.URL,
                DataConclusao = portfolio.DataConclusao,
                Imagem1 = imagem1,
                Imagem2 = imagem2
            };
        }
    }
} 