using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class MentoriaService : IMentoriaService
    {
        private readonly IMentoriaRepository _mentoriaRepository;

        public MentoriaService(IMentoriaRepository mentoriaRepository)
        {
            _mentoriaRepository = mentoriaRepository;
        }

        public async Task<IEnumerable<MentoriaReadDto>> GetAllAsync()
        {
            var mentorias = await _mentoriaRepository.GetAllAsync();
            var dtos = new List<MentoriaReadDto>();
            foreach (var mentoria in mentorias)
            {
                dtos.Add(MapToReadDto(mentoria));
            }
            return dtos;
        }

        public async Task<MentoriaReadDto> GetByIdAsync(int id)
        {
            var mentoria = await _mentoriaRepository.GetByIdAsync(id);
            return mentoria == null ? null : MapToReadDto(mentoria);
        }

        public async Task<MentoriaReadDto> CreateAsync(MentoriaCreateDto dto)
        {
            var mentoria = new Mentoria
            {
                mentor_id = dto.MentorId,
                mentorado_id = dto.MentoradoId,
                projeto_id = dto.ProjetoId,
                data_inicio = dto.DataInicio,
                data_fim = dto.DataFim,
                feedback = dto.Feedback,
                created_at = dto.CreatedAt
            };
            await _mentoriaRepository.AddAsync(mentoria);
            return MapToReadDto(mentoria);
        }

        public async Task<MentoriaReadDto> UpdateAsync(int id, MentoriaUpdateDto dto)
        {
            var mentoria = await _mentoriaRepository.GetByIdAsync(id);
            if (mentoria == null) return null;
            mentoria.data_inicio = dto.DataInicio;
            mentoria.data_fim = dto.DataFim;
            mentoria.feedback = dto.Feedback;
            _mentoriaRepository.Update(mentoria);
            return MapToReadDto(mentoria);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var mentoria = await _mentoriaRepository.GetByIdAsync(id);
            if (mentoria == null) return false;
            _mentoriaRepository.Delete(mentoria);
            return true;
        }

        private MentoriaReadDto MapToReadDto(Mentoria mentoria)
        {
            return new MentoriaReadDto
            {
                Id = mentoria.id,
                MentorId = mentoria.mentor_id,
                MentoradoId = mentoria.mentorado_id,
                ProjetoId = mentoria.projeto_id,
                DataInicio = mentoria.data_inicio,
                DataFim = mentoria.data_fim,
                Feedback = mentoria.feedback,
                CreatedAt = mentoria.created_at
            };
        }
    }
} 