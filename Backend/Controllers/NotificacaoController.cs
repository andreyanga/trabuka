using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacaoController(INotificacaoService notificacaoService)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoReadDto>>> GetAll()
        {
            var notificacoes = await _notificacaoService.GetAllAsync();
            return Ok(notificacoes);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<NotificacaoReadDto>>> GetByUsuario(int usuarioId)
        {
            var notificacoes = await _notificacaoService.GetByUsuarioAsync(usuarioId);
            return Ok(notificacoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacaoReadDto>> GetById(int id)
        {
            var notificacao = await _notificacaoService.GetByIdAsync(id);
            if (notificacao == null) return NotFound();
            return Ok(notificacao);
        }

        [HttpPost]
        public async Task<ActionResult<NotificacaoReadDto>> Create(NotificacaoCreateDto dto)
        {
            var notificacao = await _notificacaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = notificacao.Id }, notificacao);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NotificacaoReadDto>> Update(int id, NotificacaoUpdateDto dto)
        {
            var notificacao = await _notificacaoService.UpdateAsync(id, dto);
            if (notificacao == null) return NotFound();
            return Ok(notificacao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notificacaoService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 