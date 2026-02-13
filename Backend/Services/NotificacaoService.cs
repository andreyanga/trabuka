using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoService(INotificacaoRepository notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }

        public async Task<IEnumerable<NotificacaoReadDto>> GetAllAsync()
        {
            var notificacoes = await _notificacaoRepository.GetAllAsync();
            var dtos = new List<NotificacaoReadDto>();
            foreach (var notificacao in notificacoes)
            {
                dtos.Add(MapToReadDto(notificacao));
            }
            return dtos;
        }

        public async Task<IEnumerable<NotificacaoReadDto>> GetByUsuarioAsync(int usuarioId)
        {
            var notificacoes = await _notificacaoRepository.GetAllAsync();
            var filtradas = new List<NotificacaoReadDto>();
            foreach (var n in notificacoes)
            {
                if (n.UsuarioId == usuarioId)
                    filtradas.Add(MapToReadDto(n));
            }
            return filtradas;
        }

        public async Task<NotificacaoReadDto> GetByIdAsync(int id)
        {
            var notificacao = await _notificacaoRepository.GetByIdAsync(id);
            return notificacao == null ? null : MapToReadDto(notificacao);
        }

        public async Task<NotificacaoReadDto> CreateAsync(NotificacaoCreateDto dto)
        {
            var notificacao = new Notificacao
            {
                UsuarioId = dto.UsuarioId,
                Mensagem = dto.Mensagem,
                Lida = dto.Lida,
                DataEnvio = dto.DataEnvio
            };
            await _notificacaoRepository.AddAsync(notificacao);
            await _notificacaoRepository.SaveChangesAsync();
            return MapToReadDto(notificacao);
        }

        public async Task<NotificacaoReadDto> UpdateAsync(int id, NotificacaoUpdateDto dto)
        {
            var notificacao = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacao == null) return null;
            notificacao.Mensagem = dto.Mensagem;
            notificacao.Lida = dto.Lida;
            notificacao.DataEnvio = dto.DataEnvio;
            _notificacaoRepository.Update(notificacao);
            return MapToReadDto(notificacao);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var notificacao = await _notificacaoRepository.GetByIdAsync(id);
            if (notificacao == null) return false;
            _notificacaoRepository.Delete(notificacao);
            return true;
        }

        private NotificacaoReadDto MapToReadDto(Notificacao notificacao)
        {
            return new NotificacaoReadDto
            {
                Id = notificacao.NotificacaoId,
                UsuarioId = notificacao.UsuarioId,
                Mensagem = notificacao.Mensagem,
                Lida = notificacao.Lida,
                DataEnvio = notificacao.DataEnvio
            };
        }
    }
} 