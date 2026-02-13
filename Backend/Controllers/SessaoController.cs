using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly ISessaoService _service;

        public SessaoController(ISessaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessaoReadDto>>> GetAll()
        {
            var sessoes = await _service.GetAllAsync();
            return Ok(sessoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SessaoReadDto>> GetById(int id)
        {
            var sessao = await _service.GetByIdAsync(id);
            if (sessao == null) return NotFound();
            return Ok(sessao);
        }

        [HttpPost]
        public async Task<ActionResult<SessaoReadDto>> Create(SessaoCreateDto dto)
        {
            var sessao = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = sessao.Id }, sessao);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SessaoReadDto>> Update(int id, SessaoUpdateDto dto)
        {
            var sessao = await _service.UpdateAsync(id, dto);
            if (sessao == null) return NotFound();
            return Ok(sessao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 