using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracaoController : ControllerBase
    {
        private readonly IConfiguracaoService _service;

        public ConfiguracaoController(IConfiguracaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfiguracaoReadDto>>> GetAll()
        {
            var configs = await _service.GetAllAsync();
            return Ok(configs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConfiguracaoReadDto>> GetById(int id)
        {
            var config = await _service.GetByIdAsync(id);
            if (config == null) return NotFound();
            return Ok(config);
        }

        [HttpPost]
        public async Task<ActionResult<ConfiguracaoReadDto>> Create(ConfiguracaoCreateDto dto)
        {
            var config = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = config.Id }, config);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ConfiguracaoReadDto>> Update(int id, ConfiguracaoUpdateDto dto)
        {
            var config = await _service.UpdateAsync(id, dto);
            if (config == null) return NotFound();
            return Ok(config);
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