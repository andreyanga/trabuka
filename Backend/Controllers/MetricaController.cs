using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetricaController : ControllerBase
    {
        private readonly IMetricaService _service;

        public MetricaController(IMetricaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetricaReadDto>>> GetAll()
        {
            var metricas = await _service.GetAllAsync();
            return Ok(metricas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MetricaReadDto>> GetById(int id)
        {
            var metrica = await _service.GetByIdAsync(id);
            if (metrica == null) return NotFound();
            return Ok(metrica);
        }

        [HttpPost]
        public async Task<ActionResult<MetricaReadDto>> Create(MetricaCreateDto dto)
        {
            var metrica = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = metrica.Id }, metrica);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MetricaReadDto>> Update(int id, MetricaUpdateDto dto)
        {
            var metrica = await _service.UpdateAsync(id, dto);
            if (metrica == null) return NotFound();
            return Ok(metrica);
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