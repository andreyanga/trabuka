using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;
        private readonly IUsuarioService _usuarioService;

        public RelatorioController(IRelatorioService relatorioService, IUsuarioService usuarioService)
        {
            _relatorioService = relatorioService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioReadDto>>> GetAll()
        {
            var relatorios = await _relatorioService.GetAllAsync();
            return Ok(relatorios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RelatorioReadDto>> GetById(int id)
        {
            var relatorio = await _relatorioService.GetByIdAsync(id);
            if (relatorio == null) return NotFound();
            return Ok(relatorio);
        }

        /// <summary>
        /// Obtém relatórios por status (0=Pendente, 1=Aprovado, 2=Rejeitado)
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<RelatorioReadDto>>> GetByStatus(int status)
        {
            var todos = await _relatorioService.GetAllAsync();
            var filtrados = todos.Where(r => r.Status == status);
            return Ok(filtrados);
        }

        [HttpPost]
        public async Task<ActionResult<RelatorioReadDto>> Create(RelatorioCreateDto dto)
        {
            var relatorio = await _relatorioService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = relatorio.Id }, relatorio);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RelatorioReadDto>> Update(int id, RelatorioUpdateDto dto)
        {
            var relatorio = await _relatorioService.UpdateAsync(id, dto);
            if (relatorio == null) return NotFound();
            return Ok(relatorio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _relatorioService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Avalia um relatório com nota e feedback. Se nota >= 8, pode promover o nível do estudante.
        /// </summary>
        [HttpPost("{id}/avaliar")]
        public async Task<IActionResult> Avaliar(int id, AvaliarRelatorioDto dto)
        {
            var relatorio = await _relatorioService.GetByIdAsync(id);
            if (relatorio == null) return NotFound();

            // Atualizar feedback e status do relatório
            var novoStatus = dto.Nota >= 8 ? StatusRelatorio.Aprovado : StatusRelatorio.Rejeitado;

            var updateDto = new RelatorioUpdateDto
            {
                DataEnvio = relatorio.DataEnvio,
                Evidencias = relatorio.Evidencias,
                Descricao = relatorio.Descricao,
                Feedback = dto.Feedback,
                Status = (int)novoStatus
            };

            await _relatorioService.UpdateAsync(id, updateDto);

            // Se nota >= 8, tentar subir nível do estudante
            if (dto.Nota >= 8)
            {
                var usuario = await _usuarioService.GetByIdAsync(relatorio.UsuarioId);
                if (usuario != null)
                {
                    var nivelAtual = usuario.Nivel ?? NivelUsuario.Explorador;
                    NivelUsuario novoNivel = nivelAtual;
                    switch (nivelAtual)
                    {
                        case NivelUsuario.Explorador:
                            novoNivel = NivelUsuario.Praticante;
                            break;
                        case NivelUsuario.Praticante:
                            novoNivel = NivelUsuario.Construtor;
                            break;
                        case NivelUsuario.Construtor:
                            novoNivel = NivelUsuario.Mestre;
                            break;
                        case NivelUsuario.Mestre:
                            novoNivel = NivelUsuario.Mestre;
                            break;
                    }

                    if (novoNivel != nivelAtual)
                    {
                        var usuarioUpdate = new UsuarioUpdateDto
                        {
                            Nivel = novoNivel
                        };
                        await _usuarioService.UpdateAsync(usuario.UsuarioId, usuarioUpdate);
                    }
                }
            }

            return NoContent();
        }
    }
} 