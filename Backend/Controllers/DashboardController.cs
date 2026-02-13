using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém o resumo completo do dashboard para um usuário jovem
        /// </summary>
        [HttpGet("resumo/{usuarioId}")]
        public async Task<ActionResult<DashboardResumoDto>> GetDashboardResumo(int usuarioId)
        {
            try
            {
                var dashboardResumo = await _dashboardService.GetDashboardResumoAsync(usuarioId);
                return Ok(dashboardResumo);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Usuário não encontrado: {UsuarioId}", usuarioId);
                return NotFound($"Usuário com ID {usuarioId} não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dados do dashboard para usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor ao carregar dashboard");
            }
        }

        /// <summary>
        /// Obtém estatísticas rápidas do dashboard
        /// </summary>
        [HttpGet("estatisticas/{usuarioId}")]
        public async Task<ActionResult<object>> GetEstatisticasRapidas(int usuarioId)
        {
            try
            {
                var dashboardResumo = await _dashboardService.GetDashboardResumoAsync(usuarioId);
                
                var estatisticas = new
                {
                    NivelAtual = dashboardResumo.NivelNome,
                    ProgressoNivel = dashboardResumo.ProgressoNivel,
                    ProximoNivel = dashboardResumo.ProximoNivel,
                    TotalProjetos = dashboardResumo.TotalProjetos,
                    ProjetosConcluidos = dashboardResumo.ProjetosConcluidos,
                    VagasAplicadas = dashboardResumo.VagasAplicadas,
                    GanhosMesAtual = dashboardResumo.GanhosMesAtual,
                    TestesRealizados = dashboardResumo.TestesRealizados,
                    NotificacoesNaoLidas = dashboardResumo.NotificacoesNaoLidas
                };

                return Ok(estatisticas);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"Usuário com ID {usuarioId} não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar estatísticas do dashboard para usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém vagas recomendadas para o usuário
        /// </summary>
        [HttpGet("vagas-recomendadas/{usuarioId}")]
        public async Task<ActionResult<List<VagaRecomendadaDto>>> GetVagasRecomendadas(int usuarioId)
        {
            try
            {
                var dashboardResumo = await _dashboardService.GetDashboardResumoAsync(usuarioId);
                return Ok(dashboardResumo.VagasRecomendadas);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"Usuário com ID {usuarioId} não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar vagas recomendadas para usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos recentes do usuário
        /// </summary>
        [HttpGet("projetos-recentes/{usuarioId}")]
        public async Task<ActionResult<List<ProjetoResumoDto>>> GetProjetosRecentes(int usuarioId)
        {
            try
            {
                var dashboardResumo = await _dashboardService.GetDashboardResumoAsync(usuarioId);
                return Ok(dashboardResumo.ProjetosRecentes);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"Usuário com ID {usuarioId} não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos recentes para usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém pagamentos recentes do usuário
        /// </summary>
        [HttpGet("pagamentos-recentes/{usuarioId}")]
        public async Task<ActionResult<List<PagamentoResumoDto>>> GetPagamentosRecentes(int usuarioId)
        {
            try
            {
                var dashboardResumo = await _dashboardService.GetDashboardResumoAsync(usuarioId);
                return Ok(dashboardResumo.PagamentosRecentes);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"Usuário com ID {usuarioId} não encontrado");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar pagamentos recentes para usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 