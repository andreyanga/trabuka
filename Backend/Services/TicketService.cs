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
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<TicketReadDto>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets.Select(MapToReadDto);
        }

        public async Task<TicketReadDto> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new ArgumentException("Ticket não encontrado");

            return MapToReadDto(ticket);
        }

        public async Task<TicketReadDto> CreateAsync(TicketCreateDto dto)
        {
            // Validações de negócio
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentException("Descrição é obrigatória");

            var ticket = new Ticket
            {
                UsuarioId = dto.UsuarioId,
                Descricao = dto.Descricao,
                Status = (StatusTicket)dto.Status,
                DataCriacao = dto.DataCriacao == default ? DateTime.Now : dto.DataCriacao
            };

            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();

            return MapToReadDto(ticket);
        }

        public async Task<TicketReadDto> UpdateAsync(int id, TicketUpdateDto dto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new ArgumentException("Ticket não encontrado");

            // Atualizar apenas campos permitidos
            ticket.Descricao = dto.Descricao;
            ticket.Status = (StatusTicket)dto.Status;

            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            return MapToReadDto(ticket);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return false;

            _ticketRepository.Delete(ticket);
            return await _ticketRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketReadDto>> GetByUsuarioAsync(int usuarioId)
        {
            var tickets = await _ticketRepository.GetAllAsync();
            var ticketsFiltrados = tickets.Where(t => t.UsuarioId == usuarioId);
            return ticketsFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<TicketReadDto>> GetByStatusAsync(StatusTicket status)
        {
            var tickets = await _ticketRepository.GetAllAsync();
            var ticketsFiltrados = tickets.Where(t => t.Status == status);
            return ticketsFiltrados.Select(MapToReadDto);
        }

        public async Task<IEnumerable<TicketReadDto>> GetByCategoriaAsync(CategoriaTicket categoria)
        {
            // Categoria não existe no modelo Ticket, retornando todos os tickets
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets.Select(MapToReadDto);
        }

        public async Task<IEnumerable<TicketReadDto>> GetByPrioridadeAsync(PrioridadeTicket prioridade)
        {
            // Prioridade não existe no modelo Ticket, retornando todos os tickets
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets.Select(MapToReadDto);
        }

        public async Task<TicketReadDto> UpdateStatusAsync(int id, StatusTicket novoStatus)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new ArgumentException("Ticket não encontrado");

            ticket.Status = novoStatus;
            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();

            return MapToReadDto(ticket);
        }

        private static TicketReadDto MapToReadDto(Ticket ticket)
        {
            return new TicketReadDto
            {
                Id = ticket.TicketId,
                TicketId = ticket.TicketId,
                UsuarioId = ticket.UsuarioId,
                Descricao = ticket.Descricao,
                Status = (int)ticket.Status,
                DataCriacao = ticket.DataCriacao
            };
        }
    }
} 