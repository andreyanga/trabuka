using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class UsuarioEquipeService : IUsuarioEquipeService
    {
        private readonly IUsuarioEquipeRepository _usuarioEquipeRepository;

        public UsuarioEquipeService(IUsuarioEquipeRepository usuarioEquipeRepository)
        {
            _usuarioEquipeRepository = usuarioEquipeRepository;
        }

        public async Task<IEnumerable<UsuarioEquipeReadDto>> GetAllAsync()
        {
            var lista = await _usuarioEquipeRepository.GetAllAsync();
            var dtos = new List<UsuarioEquipeReadDto>();
            foreach (var item in lista)
            {
                dtos.Add(MapToReadDto(item));
            }
            return dtos;
        }

        public async Task<UsuarioEquipeReadDto> GetByIdAsync(int usuarioId, int equipeId)
        {
            var usuarioEquipe = await _usuarioEquipeRepository.GetByIdsAsync(usuarioId, equipeId);
            return usuarioEquipe == null ? null : MapToReadDto(usuarioEquipe);
        }

        public async Task<UsuarioEquipeReadDto> CreateAsync(UsuarioEquipeCreateDto dto)
        {
            var entity = new UsuarioEquipe
            {
                usuario_id = dto.UsuarioId,
                equipe_id = dto.EquipeId,
                papel = dto.Papel,
                data_entrada = dto.DataEntrada,
                created_at = dto.CreatedAt
            };
            await _usuarioEquipeRepository.AddAsync(entity);
            return MapToReadDto(entity);
        }

        public async Task<UsuarioEquipeReadDto> UpdateAsync(int usuarioId, int equipeId, UsuarioEquipeUpdateDto dto)
        {
            var usuarioEquipe = await _usuarioEquipeRepository.GetByIdsAsync(usuarioId, equipeId);
            if (usuarioEquipe == null) return null;
            usuarioEquipe.papel = dto.Papel;
            usuarioEquipe.data_entrada = dto.DataEntrada;
            _usuarioEquipeRepository.Update(usuarioEquipe);
            return MapToReadDto(usuarioEquipe);
        }

        public async Task<bool> DeleteAsync(int usuarioId, int equipeId)
        {
            var usuarioEquipe = await _usuarioEquipeRepository.GetByIdsAsync(usuarioId, equipeId);
            if (usuarioEquipe == null) return false;
            _usuarioEquipeRepository.Delete(usuarioEquipe);
            return true;
        }

        private UsuarioEquipeReadDto MapToReadDto(UsuarioEquipe entity)
        {
            return new UsuarioEquipeReadDto
            {
                UsuarioId = entity.usuario_id,
                EquipeId = entity.equipe_id,
                Papel = entity.papel,
                DataEntrada = entity.data_entrada,
                CreatedAt = entity.created_at
            };
        }
    }
} 