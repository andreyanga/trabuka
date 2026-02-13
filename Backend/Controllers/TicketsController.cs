using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os tickets
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTickets()
        {
            try
            {
                var tickets = await _ticketService.GetAllAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tickets");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um ticket por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketReadDto>> GetTicket(int id)
        {
            try
            {
                var ticket = await _ticketService.GetByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound($"Ticket com ID {id} não encontrado");
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar ticket com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo ticket
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<TicketReadDto>> CreateTicket(TicketCreateDto ticketDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var ticket = await _ticketService.CreateAsync(ticketDto);
                return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar ticket");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um ticket existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketUpdateDto ticketDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var ticket = await _ticketService.UpdateAsync(id, ticketDto);
                if (ticket == null)
                {
                    return NotFound($"Ticket com ID {id} não encontrado");
                }

                return Ok(ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar ticket com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um ticket
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                var result = await _ticketService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Ticket com ID {id} não encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar ticket com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém tickets por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTicketsPorUsuario(int usuarioId)
        {
            try
            {
                var tickets = await _ticketService.GetByUsuarioAsync(usuarioId);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tickets do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém tickets por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTicketsPorStatus(StatusTicket status)
        {
            try
            {
                var tickets = await _ticketService.GetByStatusAsync(status);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tickets por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém tickets por categoria
        /// </summary>
        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTicketsPorCategoria(CategoriaTicket categoria)
        {
            try
            {
                var tickets = await _ticketService.GetByCategoriaAsync(categoria);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tickets por categoria {Categoria}", categoria);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém tickets por prioridade
        /// </summary>
        [HttpGet("prioridade/{prioridade}")]
        public async Task<ActionResult<IEnumerable<TicketReadDto>>> GetTicketsPorPrioridade(PrioridadeTicket prioridade)
        {
            try
            {
                var tickets = await _ticketService.GetByPrioridadeAsync(prioridade);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tickets por prioridade {Prioridade}", prioridade);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza o status de um ticket
        /// </summary>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateTicketStatus(int id, StatusTicket novoStatus)
        {
            try
            {
                var ticket = await _ticketService.UpdateStatusAsync(id, novoStatus);
                if (ticket == null)
                {
                    return NotFound($"Ticket com ID {id} não encontrado");
                }

                return Ok(ticket);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do ticket com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 