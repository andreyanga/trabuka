using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        private readonly ITesteService _testeService;

        public TesteController(ITesteService testeService)
        {
            _testeService = testeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TesteReadDto>>> GetAll()
        {
            var testes = await _testeService.GetAllAsync();
            return Ok(testes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TesteReadDto>> GetById(int id)
        {
            var teste = await _testeService.GetByIdAsync(id);
            if (teste == null) return NotFound();
            return Ok(teste);
        }

        [HttpPost]
        public async Task<ActionResult<TesteReadDto>> Create(TesteCreateDto dto)
        {
            var teste = await _testeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = teste.Id }, teste);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TesteReadDto>> Update(int id, TesteUpdateDto dto)
        {
            var teste = await _testeService.UpdateAsync(id, dto);
            if (teste == null) return NotFound();
            return Ok(teste);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _testeService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // Endpoints para quiz
        [HttpGet("disponiveis/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<TesteReadDto>>> GetTestesDisponiveis(int usuarioId)
        {
            var testes = await _testeService.GetTestesDisponiveisAsync(usuarioId);
            return Ok(testes);
        }

        [HttpPost("{testeId}/iniciar")]
        public async Task<ActionResult<TesteComQuestoesDto>> IniciarTeste(int testeId, [FromBody] IniciarTesteRequest request)
        {
            try
            {
                var teste = await _testeService.IniciarTesteAsync(testeId, request.UsuarioId);
                return Ok(teste);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("submeter")]
        public async Task<ActionResult<ResultadoTesteCompletoDto>> SubmeterTeste(SubmeterTesteDto dto)
        {
            try
            {
                var resultado = await _testeService.SubmeterTesteAsync(dto);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }

    // DTO auxiliar para iniciar teste
    public class IniciarTesteRequest
    {
        public int UsuarioId { get; set; }
    }
} 