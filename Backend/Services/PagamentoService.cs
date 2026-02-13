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
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public PagamentoService(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetAllAsync()
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            return pagamentos.Select(MapToReadDto);
        }

        public async Task<PagamentoReadDto> GetByIdAsync(int id)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);
            if (pagamento == null)
                throw new ArgumentException("Pagamento não encontrado");

            return MapToReadDto(pagamento);
        }

        public async Task<PagamentoReadDto> CreateAsync(PagamentoCreateDto dto)
        {
            // Validações de negócio
            if (dto.Valor <= 0)
                throw new ArgumentException("Valor deve ser maior que zero");

            if (dto.Data <= DateTime.MinValue)
                throw new ArgumentException("Data inválida");

            var pagamento = new Pagamento
            {
                UsuarioId = dto.UsuarioId,
                EmpresaId = dto.EmpresaId,
                Valor = dto.Valor,
                Data = dto.Data,
                Status = (StatusPagamento)dto.Status
            };

            await _pagamentoRepository.AddAsync(pagamento);
            await _pagamentoRepository.SaveChangesAsync();

            return MapToReadDto(pagamento);
        }

        public async Task<PagamentoReadDto> UpdateAsync(int id, PagamentoUpdateDto dto)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);
            if (pagamento == null)
                throw new ArgumentException("Pagamento não encontrado");

            // Atualizar apenas campos permitidos
            pagamento.Valor = dto.Valor;
            pagamento.Data = dto.Data;
            pagamento.Status = (StatusPagamento)dto.Status;

            _pagamentoRepository.Update(pagamento);
            await _pagamentoRepository.SaveChangesAsync();

            return MapToReadDto(pagamento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pagamento = await _pagamentoRepository.GetByIdAsync(id);
            if (pagamento == null)
                return false;

            _pagamentoRepository.Delete(pagamento);
            return await _pagamentoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetByUsuarioAsync(int usuarioId)
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            var pagamentosFiltrados = pagamentos.Where(p => p.UsuarioId == usuarioId);
            return pagamentosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetByEmpresaAsync(int empresaId)
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            var pagamentosFiltrados = pagamentos.Where(p => p.EmpresaId == empresaId);
            return pagamentosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetByStatusAsync(StatusPagamento status)
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            var pagamentosFiltrados = pagamentos.Where(p => p.Status == status);
            return pagamentosFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetByTipoAsync(TipoPagamento tipo)
        {
            // Tipo não existe no modelo Pagamento, retornando todos os pagamentos
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            return pagamentos.Select(MapToReadDto);
        }

        public async Task<IEnumerable<PagamentoReadDto>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            var pagamentos = await _pagamentoRepository.GetAllAsync();
            var pagamentosFiltrados = pagamentos.Where(p => p.Data >= dataInicio && p.Data <= dataFim);
            return pagamentosFiltrados.Select(MapToReadDto);
        }

        private static PagamentoReadDto MapToReadDto(Pagamento pagamento)
        {
            return new PagamentoReadDto
            {
                Id = pagamento.PagamentoId,
                PagamentoId = pagamento.PagamentoId,
                UsuarioId = pagamento.UsuarioId,
                EmpresaId = pagamento.EmpresaId,
                Valor = pagamento.Valor,
                Data = pagamento.Data,
                Status = (int)pagamento.Status
            };
        }
    }
} 