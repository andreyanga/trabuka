using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Models;
using TrabukaApi.Helpers;
using TrabukaApi.Data;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using TrabukaApi.Dtos;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagemProjetoController : ControllerBase
    {
        private readonly TrabukaDbContext _context;
        private readonly ILogger<ImagemProjetoController> _logger;

        public ImagemProjetoController(TrabukaDbContext context, ILogger<ImagemProjetoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Faz upload de uma imagem para um projeto de portfólio
        /// </summary>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImagem([FromForm] ImagemProjetoUploadDto dto)
        {
            try
            {
                if (dto.Imagem == null || dto.Imagem.Length == 0)
                    return BadRequest("Arquivo de imagem inválido.");

                var caminho = await FileUploadHelper.SaveImageAsync(dto.Imagem, "projetos");
                var img = new ImagemProjeto
                {
                    PortfolioId = dto.PortfolioId,
                    imagem_url = caminho,
                    Descricao = dto.Descricao ?? string.Empty,
                    created_at = DateTime.UtcNow
                };
                _context.ImagensProjetos.Add(img);
                await _context.SaveChangesAsync();
                return Ok(img);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da imagem de projeto");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 