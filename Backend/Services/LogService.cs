using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class LogService : ILogService
    {
        private readonly Data.TrabukaDbContext _context;

        public LogService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogReadDto>> GetAllAsync()
        {
            var logs = _context.Logs;
            var dtos = new List<LogReadDto>();
            foreach (var log in logs)
            {
                dtos.Add(MapToReadDto(log));
            }
            return await Task.FromResult(dtos);
        }

        public async Task<LogReadDto> GetByIdAsync(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            return log == null ? null : MapToReadDto(log);
        }

        public async Task<LogReadDto> CreateAsync(LogCreateDto dto)
        {
            var log = new Log
            {
                usuario_id = dto.UsuarioId,
                acao = dto.Acao,
                detalhes = dto.Detalhes,
                ip = dto.Ip,
                data_hora = dto.DataHora
            };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return MapToReadDto(log);
        }

        public async Task<LogReadDto> UpdateAsync(int id, LogUpdateDto dto)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null) return null;
            log.acao = dto.Acao;
            log.detalhes = dto.Detalhes;
            log.ip = dto.Ip;
            log.data_hora = dto.DataHora;
            await _context.SaveChangesAsync();
            return MapToReadDto(log);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null) return false;
            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }

        private LogReadDto MapToReadDto(Log log)
        {
            return new LogReadDto
            {
                Id = log.id,
                UsuarioId = log.usuario_id,
                Acao = log.acao,
                Detalhes = log.detalhes,
                Ip = log.ip,
                DataHora = log.data_hora
            };
        }
    }
} 