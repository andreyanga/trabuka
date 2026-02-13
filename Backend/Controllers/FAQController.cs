using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FAQReadDto>>> GetAll()
        {
            var faqs = await _faqService.GetAllAsync();
            return Ok(faqs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FAQReadDto>> GetById(int id)
        {
            var faq = await _faqService.GetByIdAsync(id);
            if (faq == null) return NotFound();
            return Ok(faq);
        }

        [HttpPost]
        public async Task<ActionResult<FAQReadDto>> Create(FAQCreateDto dto)
        {
            var faq = await _faqService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = faq.Id }, faq);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FAQReadDto>> Update(int id, FAQUpdateDto dto)
        {
            var faq = await _faqService.UpdateAsync(id, dto);
            if (faq == null) return NotFound();
            return Ok(faq);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _faqService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 