using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioEquipeController : ControllerBase
    {
        private readonly IUsuarioEquipeService _service;

        public UsuarioEquipeController(IUsuarioEquipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioEquipeReadDto>>> GetAll()
        {
            var lista = await _service.GetAllAsync();
            return Ok(lista);
        }

        [HttpGet("{usuarioId}/{equipeId}")]
        public async Task<ActionResult<UsuarioEquipeReadDto>> GetById(int usuarioId, int equipeId)
        {
            var item = await _service.GetByIdAsync(usuarioId, equipeId);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioEquipeReadDto>> Create(UsuarioEquipeCreateDto dto)
        {
            var item = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { usuarioId = item.UsuarioId, equipeId = item.EquipeId }, item);
        }

        [HttpPut("{usuarioId}/{equipeId}")]
        public async Task<ActionResult<UsuarioEquipeReadDto>> Update(int usuarioId, int equipeId, UsuarioEquipeUpdateDto dto)
        {
            var item = await _service.UpdateAsync(usuarioId, equipeId, dto);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpDelete("{usuarioId}/{equipeId}")]
        public async Task<IActionResult> Delete(int usuarioId, int equipeId)
        {
            var result = await _service.DeleteAsync(usuarioId, equipeId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 