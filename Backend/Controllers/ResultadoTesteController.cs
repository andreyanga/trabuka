using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultadoTesteController : ControllerBase
    {
        private readonly IResultadoTesteService _resultadoTesteService;

        public ResultadoTesteController(IResultadoTesteService resultadoTesteService)
        {
            _resultadoTesteService = resultadoTesteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadoTesteReadDto>>> GetAll()
        {
            var resultados = await _resultadoTesteService.GetAllAsync();
            return Ok(resultados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultadoTesteReadDto>> GetById(int id)
        {
            var resultado = await _resultadoTesteService.GetByIdAsync(id);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<ActionResult<ResultadoTesteReadDto>> Create(ResultadoTesteCreateDto dto)
        {
            var resultado = await _resultadoTesteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = resultado.Id }, resultado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResultadoTesteReadDto>> Update(int id, ResultadoTesteUpdateDto dto)
        {
            var resultado = await _resultadoTesteService.UpdateAsync(id, dto);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _resultadoTesteService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 