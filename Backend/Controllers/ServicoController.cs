using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _service;

        public ServicoController(IServicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicoReadDto>>> GetAll()
        {
            var servicos = await _service.GetAllAsync();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoReadDto>> GetById(int id)
        {
            var servico = await _service.GetByIdAsync(id);
            if (servico == null) return NotFound();
            return Ok(servico);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoReadDto>> Create(ServicoCreateDto dto)
        {
            var servico = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServicoReadDto>> Update(int id, ServicoUpdateDto dto)
        {
            var servico = await _service.UpdateAsync(id, dto);
            if (servico == null) return NotFound();
            return Ok(servico);
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