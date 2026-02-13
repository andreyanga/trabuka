using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;

        public EquipeService(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<IEnumerable<EquipeReadDto>> GetAllAsync()
        {
            var equipes = await _equipeRepository.GetAllAsync();
            var dtos = new List<EquipeReadDto>();
            foreach (var equipe in equipes)
            {
                dtos.Add(MapToReadDto(equipe));
            }
            return dtos;
        }

        public async Task<EquipeReadDto> GetByIdAsync(int id)
        {
            var equipe = await _equipeRepository.GetByIdAsync(id);
            return equipe == null ? null : MapToReadDto(equipe);
        }

        public async Task<EquipeReadDto> CreateAsync(EquipeCreateDto dto)
        {
            var equipe = new Equipe
            {
                nome = dto.Nome,
                projeto_id = dto.ProjetoId,
                data_criacao = dto.DataCriacao,
                status = dto.Status
            };
            await _equipeRepository.AddAsync(equipe);
            return MapToReadDto(equipe);
        }

        public async Task<EquipeReadDto> UpdateAsync(int id, EquipeUpdateDto dto)
        {
            var equipe = await _equipeRepository.GetByIdAsync(id);
            if (equipe == null) return null;
            equipe.nome = dto.Nome;
            equipe.data_criacao = dto.DataCriacao;
            equipe.status = dto.Status;
            _equipeRepository.Update(equipe);
            return MapToReadDto(equipe);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var equipe = await _equipeRepository.GetByIdAsync(id);
            if (equipe == null) return false;
            _equipeRepository.Delete(equipe);
            return true;
        }

        private EquipeReadDto MapToReadDto(Equipe equipe)
        {
            return new EquipeReadDto
            {
                Id = equipe.id,
                Nome = equipe.nome,
                ProjetoId = equipe.projeto_id,
                DataCriacao = equipe.data_criacao,
                Status = equipe.status
            };
        }
    }
} 