using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;

namespace TrabukaApi.Services
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _faqRepository;

        public FAQService(IFAQRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public async Task<IEnumerable<FAQReadDto>> GetAllAsync()
        {
            var faqs = await _faqRepository.GetAllAsync();
            var dtos = new List<FAQReadDto>();
            foreach (var faq in faqs)
            {
                dtos.Add(MapToReadDto(faq));
            }
            return dtos;
        }

        public async Task<FAQReadDto> GetByIdAsync(int id)
        {
            var faq = await _faqRepository.GetByIdAsync(id);
            return faq == null ? null : MapToReadDto(faq);
        }

        public async Task<FAQReadDto> CreateAsync(FAQCreateDto dto)
        {
            var faq = new FAQ
            {
                Pergunta = dto.Pergunta,
                Resposta = dto.Resposta
            };
            await _faqRepository.AddAsync(faq);
            return MapToReadDto(faq);
        }

        public async Task<FAQReadDto> UpdateAsync(int id, FAQUpdateDto dto)
        {
            var faq = await _faqRepository.GetByIdAsync(id);
            if (faq == null) return null;
            faq.Pergunta = dto.Pergunta;
            faq.Resposta = dto.Resposta;
            _faqRepository.Update(faq);
            return MapToReadDto(faq);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var faq = await _faqRepository.GetByIdAsync(id);
            if (faq == null) return false;
            _faqRepository.Delete(faq);
            return true;
        }

        private FAQReadDto MapToReadDto(FAQ faq)
        {
            return new FAQReadDto
            {
                Id = faq.FAQId,
                Pergunta = faq.Pergunta,
                Resposta = faq.Resposta
            };
        }
    }
} 