using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipeController : ControllerBase
    {
        private readonly IEquipeService _equipeService;

        public EquipeController(IEquipeService equipeService)
        {
            _equipeService = equipeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipeReadDto>>> GetAll()
        {
            var equipes = await _equipeService.GetAllAsync();
            return Ok(equipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipeReadDto>> GetById(int id)
        {
            var equipe = await _equipeService.GetByIdAsync(id);
            if (equipe == null) return NotFound();
            return Ok(equipe);
        }

        [HttpPost]
        public async Task<ActionResult<EquipeReadDto>> Create(EquipeCreateDto dto)
        {
            var equipe = await _equipeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = equipe.Id }, equipe);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EquipeReadDto>> Update(int id, EquipeUpdateDto dto)
        {
            var equipe = await _equipeService.UpdateAsync(id, dto);
            if (equipe == null) return NotFound();
            return Ok(equipe);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _equipeService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 