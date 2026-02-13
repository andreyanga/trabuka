using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<EmpresaReadDto>> GetAllAsync()
        {
            var empresas = await _empresaRepository.GetAllAsync();
            return empresas.Select(MapToReadDto);
        }

        public async Task<EmpresaReadDto> GetByIdAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null)
                throw new ArgumentException("Empresa não encontrada");

            return MapToReadDto(empresa);
        }

        public async Task<EmpresaReadDto> CreateAsync(EmpresaCreateDto dto)
        {
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email é obrigatório");

            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome é obrigatório");

            var empresa = new Empresa
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Setor = dto.Setor ?? "",
                Contato = dto.Contato ?? ""
            };

            await _empresaRepository.AddAsync(empresa);
            await _empresaRepository.SaveChangesAsync();

            return MapToReadDto(empresa);
        }

        public async Task<EmpresaReadDto> UpdateAsync(int id, EmpresaUpdateDto dto)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null)
                throw new ArgumentException("Empresa não encontrada");

            // Atualizar apenas campos permitidos
            empresa.Nome = dto.Nome;
            empresa.Setor = dto.Setor ?? empresa.Setor;
            empresa.Contato = dto.Contato ?? empresa.Contato;

            _empresaRepository.Update(empresa);
            await _empresaRepository.SaveChangesAsync();

            return MapToReadDto(empresa);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empresa = await _empresaRepository.GetByIdAsync(id);
            if (empresa == null)
                return false;

            _empresaRepository.Delete(empresa);
            return await _empresaRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmpresaReadDto>> GetBySetorAsync(TrabukaApi.Models.Enums.SetorEmpresa setor)
        {
            var empresas = await _empresaRepository.GetAllAsync();
            var empresasFiltradas = empresas.Where(e => e.Setor == setor.ToString());
            return empresasFiltradas.Select(MapToReadDto);
        }

        public async Task<IEnumerable<EmpresaReadDto>> GetByStatusAsync(TrabukaApi.Models.Enums.StatusEmpresa status)
        {
            // Status não existe no modelo Empresa, retornando todas as empresas
            var empresas = await _empresaRepository.GetAllAsync();
            return empresas.Select(MapToReadDto);
        }

        public async Task<IEnumerable<EmpresaReadDto>> GetByLocalizacaoAsync(string provincia)
        {
            // Localizacao não existe no modelo Empresa, retornando todas as empresas
            var empresas = await _empresaRepository.GetAllAsync();
            return empresas.Select(MapToReadDto);
        }

        private static EmpresaReadDto MapToReadDto(Empresa empresa)
        {
            return new EmpresaReadDto
            {
                Id = empresa.EmpresaId,
                EmpresaId = empresa.EmpresaId,
                Nome = empresa.Nome,
                Email = empresa.Email,
                Setor = empresa.Setor,
                Contato = empresa.Contato
            };
        }
    }
} 