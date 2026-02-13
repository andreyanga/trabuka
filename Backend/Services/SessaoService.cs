using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly Data.TrabukaDbContext _context;

        public SessaoService(Data.TrabukaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SessaoReadDto>> GetAllAsync()
        {
            var sessoes = _context.Sessoes;
            var dtos = new List<SessaoReadDto>();
            foreach (var sessao in sessoes)
            {
                dtos.Add(MapToReadDto(sessao));
            }
            return await Task.FromResult(dtos);
        }

        public async Task<SessaoReadDto> GetByIdAsync(int id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            return sessao == null ? null : MapToReadDto(sessao);
        }

        public async Task<SessaoReadDto> CreateAsync(SessaoCreateDto dto)
        {
            var sessao = new Sessao
            {
                usuario_id = dto.UsuarioId,
                token = dto.Token,
                data_inicio = dto.DataInicio,
                data_expiracao = dto.DataExpiracao,
                dispositivo = dto.Dispositivo,
                status = dto.Status
            };
            _context.Sessoes.Add(sessao);
            await _context.SaveChangesAsync();
            return MapToReadDto(sessao);
        }

        public async Task<SessaoReadDto> UpdateAsync(int id, SessaoUpdateDto dto)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao == null) return null;
            sessao.data_expiracao = dto.DataExpiracao;
            sessao.dispositivo = dto.Dispositivo;
            sessao.status = dto.Status;
            await _context.SaveChangesAsync();
            return MapToReadDto(sessao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao == null) return false;
            _context.Sessoes.Remove(sessao);
            await _context.SaveChangesAsync();
            return true;
        }

        private SessaoReadDto MapToReadDto(Sessao sessao)
        {
            return new SessaoReadDto
            {
                Id = sessao.id,
                UsuarioId = sessao.usuario_id,
                Token = sessao.token,
                DataInicio = sessao.data_inicio,
                DataExpiracao = sessao.data_expiracao,
                Dispositivo = sessao.dispositivo,
                Status = sessao.status
            };
        }
    }
} 