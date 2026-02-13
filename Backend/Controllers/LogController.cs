using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _service;

        public LogController(ILogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogReadDto>>> GetAll()
        {
            var logs = await _service.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogReadDto>> GetById(int id)
        {
            var log = await _service.GetByIdAsync(id);
            if (log == null) return NotFound();
            return Ok(log);
        }

        [HttpPost]
        public async Task<ActionResult<LogReadDto>> Create(LogCreateDto dto)
        {
            var log = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LogReadDto>> Update(int id, LogUpdateDto dto)
        {
            var log = await _service.UpdateAsync(id, dto);
            if (log == null) return NotFound();
            return Ok(log);
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