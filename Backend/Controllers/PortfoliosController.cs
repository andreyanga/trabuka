using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfoliosController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ILogger<PortfoliosController> _logger;

        public PortfoliosController(IPortfolioService portfolioService, ILogger<PortfoliosController> logger)
        {
            _portfolioService = portfolioService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os portfolios
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioReadDto>>> GetPortfolios()
        {
            try
            {
                var portfolios = await _portfolioService.GetAllAsync();
                return Ok(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolios");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um portfolio por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioReadDto>> GetPortfolio(int id)
        {
            try
            {
                var portfolio = await _portfolioService.GetByIdAsync(id);
                if (portfolio == null)
                {
                    return NotFound($"Portfolio com ID {id} não encontrado");
                }
                return Ok(portfolio);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolio com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo portfolio
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PortfolioReadDto>> CreatePortfolio([FromForm] PortfolioCreateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string imagem1Path = null;
                string imagem2Path = null;
                if (dto.Imagem1 != null && dto.Imagem1.Length > 0)
                {
                    imagem1Path = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem1, "portfolios");
                }
                if (dto.Imagem2 != null && dto.Imagem2.Length > 0)
                {
                    imagem2Path = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem2, "portfolios");
                }

                var portfolioDto = new PortfolioCreateDto
                {
                    UsuarioId = dto.UsuarioId,
                    URL = dto.URL,
                    DataConclusao = dto.DataConclusao,
                    Imagem1 = imagem1Path,
                    Imagem2 = imagem2Path
                };

                var portfolio = await _portfolioService.CreateAsync(portfolioDto);
                return CreatedAtAction(nameof(GetPortfolio), new { id = portfolio.Id }, portfolio);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar portfolio");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um portfolio existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePortfolio(int id, PortfolioUpdateDto portfolioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var portfolio = await _portfolioService.UpdateAsync(id, portfolioDto);
                if (portfolio == null)
                {
                    return NotFound($"Portfolio com ID {id} não encontrado");
                }

                return Ok(portfolio);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar portfolio com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um portfólio existente (com upload de imagens)
        /// </summary>
        [HttpPut("{id}/upload")]
        public async Task<IActionResult> UpdatePortfolioComUpload(int id, [FromForm] PortfolioUpdateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string imagem1Path = null;
                string imagem2Path = null;
                if (dto.Imagem1 != null && dto.Imagem1.Length > 0)
                {
                    imagem1Path = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem1, "portfolios");
                }
                if (dto.Imagem2 != null && dto.Imagem2.Length > 0)
                {
                    imagem2Path = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem2, "portfolios");
                }

                var portfolioUpdateDto = new PortfolioUpdateDto
                {
                    URL = dto.URL,
                    DataConclusao = dto.DataConclusao,
                    Imagem1 = imagem1Path,
                    Imagem2 = imagem2Path
                };

                var portfolio = await _portfolioService.UpdateAsync(id, portfolioUpdateDto);
                if (portfolio == null)
                {
                    return NotFound($"Portfolio com ID {id} não encontrado");
                }

                return Ok(portfolio);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar portfolio com upload");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um portfolio
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            try
            {
                var result = await _portfolioService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Portfolio com ID {id} não encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar portfolio com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém portfolios por usuário
        /// </summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PortfolioReadDto>>> GetPortfoliosPorUsuario(int usuarioId)
        {
            try
            {
                var portfolios = await _portfolioService.GetByUsuarioAsync(usuarioId);
                return Ok(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolios do usuário {UsuarioId}", usuarioId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém portfolios por categoria
        /// </summary>
        [HttpGet("categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<PortfolioReadDto>>> GetPortfoliosPorCategoria(CategoriaPortfolio categoria)
        {
            try
            {
                var portfolios = await _portfolioService.GetByCategoriaAsync(categoria);
                return Ok(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolios por categoria {Categoria}", categoria);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém portfolios por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<PortfolioReadDto>>> GetPortfoliosPorStatus(StatusPortfolio status)
        {
            try
            {
                var portfolios = await _portfolioService.GetByStatusAsync(status);
                return Ok(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolios por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém portfolios por tecnologia
        /// </summary>
        [HttpGet("tecnologia/{tecnologia}")]
        public async Task<ActionResult<IEnumerable<PortfolioReadDto>>> GetPortfoliosPorTecnologia(string tecnologia)
        {
            try
            {
                var portfolios = await _portfolioService.GetByTecnologiaAsync(tecnologia);
                return Ok(portfolios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar portfolios por tecnologia {Tecnologia}", tecnologia);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza o status de um portfolio
        /// </summary>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdatePortfolioStatus(int id, StatusPortfolio novoStatus)
        {
            try
            {
                var portfolio = await _portfolioService.UpdateStatusAsync(id, novoStatus);
                if (portfolio == null)
                {
                    return NotFound($"Portfolio com ID {id} não encontrado");
                }

                return Ok(portfolio);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar status do portfolio com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Faz upload de uma imagem do portfolio
        /// </summary>
        [HttpPost("{id}/upload-imagem")]
        public async Task<IActionResult> UploadImagemPortfolio(int id, [FromForm] PortfolioImagemUploadDto dto)
        {
            try
            {
                if (dto.Imagem == null || dto.Imagem.Length == 0)
                    return BadRequest("Arquivo de imagem inválido.");
                if (dto.Posicao != 1 && dto.Posicao != 2)
                    return BadRequest("Posição da imagem deve ser 1 ou 2.");

                var caminho = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem, "portfolios");
                var portfolio = await _portfolioService.GetByIdAsync(id);
                if (portfolio == null)
                    return NotFound($"Portfolio com ID {id} não encontrado");

                // Atualiza o campo Imagem1 ou Imagem2
                await _portfolioService.AtualizarImagemAsync(id, caminho, dto.Posicao);
                return Ok(new { imagem = caminho });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da imagem do portfolio {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 