using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosRelacionadosController : ControllerBase
    {
        private readonly IServicosRelacionadosService _service;

        public ServicosRelacionadosController(IServicosRelacionadosService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicosRelacionadosReadDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{servicoId}/{relacionadoId}")]
        public async Task<ActionResult<ServicosRelacionadosReadDto>> GetById(int servicoId, int relacionadoId)
        {
            var result = await _service.GetByIdAsync(servicoId, relacionadoId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServicosRelacionadosReadDto>> Create(ServicosRelacionadosCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { servicoId = created.ServicoId, relacionadoId = created.RelacionadoId }, created);
        }

        [HttpPut("{servicoId}/{relacionadoId}")]
        public async Task<ActionResult<ServicosRelacionadosReadDto>> Update(int servicoId, int relacionadoId, ServicosRelacionadosUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(servicoId, relacionadoId, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{servicoId}/{relacionadoId}")]
        public async Task<IActionResult> Delete(int servicoId, int relacionadoId)
        {
            var deleted = await _service.DeleteAsync(servicoId, relacionadoId);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 