using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentoriaController : ControllerBase
    {
        private readonly IMentoriaService _mentoriaService;

        public MentoriaController(IMentoriaService mentoriaService)
        {
            _mentoriaService = mentoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MentoriaReadDto>>> GetAll()
        {
            var mentorias = await _mentoriaService.GetAllAsync();
            return Ok(mentorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MentoriaReadDto>> GetById(int id)
        {
            var mentoria = await _mentoriaService.GetByIdAsync(id);
            if (mentoria == null) return NotFound();
            return Ok(mentoria);
        }

        [HttpPost]
        public async Task<ActionResult<MentoriaReadDto>> Create(MentoriaCreateDto dto)
        {
            var mentoria = await _mentoriaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = mentoria.Id }, mentoria);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MentoriaReadDto>> Update(int id, MentoriaUpdateDto dto)
        {
            var mentoria = await _mentoriaService.UpdateAsync(id, dto);
            if (mentoria == null) return NotFound();
            return Ok(mentoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mentoriaService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 