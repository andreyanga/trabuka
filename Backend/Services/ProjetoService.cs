using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetAllAsync()
        {
            var projetos = await _projetoRepository.GetAllAsync();
            return projetos.Select(MapToReadDto);
        }

        public async Task<ProjetoReadDto> GetByIdAsync(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
                throw new ArgumentException("Projeto não encontrado");

            return MapToReadDto(projeto);
        }

        public async Task<ProjetoReadDto> CreateAsync(ProjetoCreateDto dto)
        {
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória");

            if (dto.Oramento <= 0)
                throw new ArgumentException("Orçamento deve ser maior que zero");

            if (dto.Prazo <= DateTime.Now)
                throw new ArgumentException("Prazo deve ser futuro");

            var projeto = new Projeto
            {
                Descricao = dto.Descricao,
                Tipo = dto.Tipo ?? "",
                Oramento = dto.Oramento,
                Horario = dto.Horario,
                Prazo = dto.Prazo,
                EmpresaId = dto.EmpresaId,
                MentorId = dto.MentorId,
                Status = (StatusProjeto)dto.Status,
                ImagemCapa = dto.ImagemCapa ?? ""
            };

            await _projetoRepository.AddAsync(projeto);
            await _projetoRepository.SaveChangesAsync();

            return MapToReadDto(projeto);
        }

        public async Task<ProjetoReadDto> UpdateAsync(int id, ProjetoUpdateDto dto)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
                throw new ArgumentException("Projeto não encontrado");

            // Atualizar apenas campos permitidos
            projeto.Descricao = dto.Descricao;
            projeto.Tipo = dto.Tipo ?? projeto.Tipo;
            projeto.Oramento = dto.Oramento;
            projeto.Horario = dto.Horario;
            projeto.Prazo = dto.Prazo;
            projeto.MentorId = dto.MentorId;
            projeto.Status = (StatusProjeto)dto.Status;
            projeto.ImagemCapa = dto.ImagemCapa ?? projeto.ImagemCapa;

            _projetoRepository.Update(projeto);
            await _projetoRepository.SaveChangesAsync();

            return MapToReadDto(projeto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
                return false;

            _projetoRepository.Delete(projeto);
            return await _projetoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetByEmpresaAsync(int empresaId)
        {
            var projetos = await _projetoRepository.GetAllAsync();
            var projetosFiltrados = projetos.Where(p => p.EmpresaId == empresaId);
            return projetosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetByStatusAsync(StatusProjeto status)
        {
            var projetos = await _projetoRepository.GetAllAsync();
            var projetosFiltrados = projetos.Where(p => p.Status == status);
            return projetosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetByTipoAsync(TipoProjeto tipo)
        {
            var projetos = await _projetoRepository.GetAllAsync();
            var projetosFiltrados = projetos.Where(p => p.Tipo == tipo.ToString());
            return projetosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetByLocalizacaoAsync(string provincia)
        {
            // Localizacao não existe no modelo Projeto, retornando todos os projetos
            var projetos = await _projetoRepository.GetAllAsync();
            return projetos.Select(MapToReadDto);
        }

        public async Task<IEnumerable<ProjetoReadDto>> GetByFaixaSalarialAsync(decimal minSalario, decimal maxSalario)
        {
            var projetos = await _projetoRepository.GetAllAsync();
            var projetosFiltrados = projetos.Where(p => p.Oramento >= minSalario && p.Oramento <= maxSalario);
            return projetosFiltrados.Select(MapToReadDto);
        }

        public async Task<ProjetoReadDto> AtualizarImagemCapaAsync(int id, string caminhoImagem)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null) return null;
            projeto.ImagemCapa = caminhoImagem;
            await _projetoRepository.SaveChangesAsync();
            return MapToReadDto(projeto);
        }

        private static ProjetoReadDto MapToReadDto(Projeto projeto)
        {
            // Corrigir caminho da imagem de capa
            var imagemCapa = projeto.ImagemCapa;
            if (!string.IsNullOrEmpty(imagemCapa))
            {
                // Se o caminho ainda contém "src/", remover e usar apenas o nome do arquivo
                if (imagemCapa.Contains("src/"))
                {
                    imagemCapa = Path.GetFileName(imagemCapa);
                }
                
                // Adicionar o caminho completo para acesso direto
                imagemCapa = $"/assets/images/projetos/{imagemCapa}";
            }

            return new ProjetoReadDto
            {
                Id = projeto.ProjetoId,
                ProjetoId = projeto.ProjetoId,
                Descricao = projeto.Descricao,
                Tipo = projeto.Tipo,
                Oramento = projeto.Oramento,
                Horario = projeto.Horario,
                Prazo = projeto.Prazo,
                EmpresaId = projeto.EmpresaId,
                MentorId = projeto.MentorId,
                Status = (int)projeto.Status,
                ImagemCapa = imagemCapa
            };
        }
    }
} 