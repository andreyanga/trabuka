using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController : ControllerBase
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly ILogger<PagamentosController> _logger;

        public PagamentosController(IPagamentoService pagamentoService, ILogger<PagamentosController> logger)
        {
            _pagamentoService = pagamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os pagamentos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentos()
        {
            try
            {
                var pagamentos = await _pagamentoService.GetAllAsync();
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um pagamento por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoReadDto>> GetPagamento(int id)
        {
            try
            {
                var pagamento = await _pagamentoService.GetByIdAsync(id);
                if (pagamento == null)
                {
                    return NotFound($"Pagamento com ID {id} não encontrado");
                }
                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamento com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo pagamento
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PagamentoReadDto>> CreatePagamento(PagamentoCreateDto pagamentoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pagamento = await _pagamentoService.CreateAsync(pagamentoDto);
                return CreatedAtAction(nameof(GetPagamento), new { id = pagamento.Id }, pagamento);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pagamento");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um pagamento existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePagamento(int id, PagamentoUpdateDto pagamentoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var pagamento = await _pagamentoService.UpdateAsync(id, pagamentoDto);
                if (pagamento == null)
                {
                    return NotFound($"Pagamento com ID {id} não encontrado");
                }

                return Ok(pagamento);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pagamento com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um pagamento
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamento(int id)
        {
            try
            {
                var result = await _pagamentoService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Pagamento com ID {id} não encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar pagamento com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentosPorUsuario(int usuarioId)
        {
            try
            {
                var pagamentos = await _pagamentoService.GetByUsuarioAsync(usuarioId);
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos por empresa
        /// </summary>
        [HttpGet("empresa/{empresaId}")]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentosPorEmpresa(int empresaId)
        {
            try
            {
                var pagamentos = await _pagamentoService.GetByEmpresaAsync(empresaId);
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos da empresa {EmpresaId}", empresaId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentosPorStatus(StatusPagamento status)
        {
            try
            {
                var pagamentos = await _pagamentoService.GetByStatusAsync(status);
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentosPorTipo(TipoPagamento tipo)
        {
            try
            {
                var pagamentos = await _pagamentoService.GetByTipoAsync(tipo);
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos por período
        /// </summary>
        [HttpGet("periodo/{dataInicio}/{dataFim}")]
        public async Task<ActionResult<IEnumerable<PagamentoReadDto>>> GetPagamentosPorPeriodo(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var pagamentos = await _pagamentoService.GetByPeriodoAsync(dataInicio, dataFim);
                return Ok(pagamentos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos por período {DataInicio}-{DataFim}", dataInicio, dataFim);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 