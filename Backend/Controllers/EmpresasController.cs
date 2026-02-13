using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly ILogger<EmpresasController> _logger;

        public EmpresasController(IEmpresaService empresaService, ILogger<EmpresasController> logger)
        {
            _empresaService = empresaService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todas as empresas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetEmpresas()
        {
            try
            {
                var empresas = await _empresaService.GetAllAsync();
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresas");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém uma empresa por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmpresaReadDto>> GetEmpresa(int id)
        {
            try
            {
                var empresa = await _empresaService.GetByIdAsync(id);
                if (empresa == null)
                {
                    return NotFound($"Empresa com ID {id} não encontrada");
                }
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresa com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria uma nova empresa
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EmpresaReadDto>> CreateEmpresa(EmpresaCreateDto empresaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var empresa = await _empresaService.CreateAsync(empresaDto);
                return CreatedAtAction(nameof(GetEmpresa), new { id = empresa.Id }, empresa);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar empresa");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza uma empresa existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(int id, EmpresaUpdateDto empresaDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var empresa = await _empresaService.UpdateAsync(id, empresaDto);
                if (empresa == null)
                {
                    return NotFound($"Empresa com ID {id} não encontrada");
                }

                return Ok(empresa);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar empresa com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove uma empresa
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            try
            {
                var result = await _empresaService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Empresa com ID {id} não encontrada");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar empresa com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém empresas por setor
        /// </summary>
        [HttpGet("setor/{setor}")]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetEmpresasPorSetor(SetorEmpresa setor)
        {
            try
            {
                var empresas = await _empresaService.GetBySetorAsync(setor);
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresas por setor {Setor}", setor);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém empresas por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetEmpresasPorStatus(StatusEmpresa status)
        {
            try
            {
                var empresas = await _empresaService.GetByStatusAsync(status);
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresas por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém empresas por localização
        /// </summary>
        [HttpGet("localizacao/{provincia}")]
        public async Task<ActionResult<IEnumerable<EmpresaReadDto>>> GetEmpresasPorLocalizacao(string provincia)
        {
            try
            {
                var empresas = await _empresaService.GetByLocalizacaoAsync(provincia);
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empresas por localização {Provincia}", provincia);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 