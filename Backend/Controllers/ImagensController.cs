using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagensController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ImagensController> _logger;

        public ImagensController(IWebHostEnvironment environment, ILogger<ImagensController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        /// <summary>
        /// Serve uma imagem específica
        /// </summary>
        [HttpGet("{tipo}/{nomeArquivo}")]
        public IActionResult GetImagem(string tipo, string nomeArquivo)
        {
            try
            {
                // Construir o caminho para a imagem
                var caminhoImagem = Path.Combine(_environment.WebRootPath, "assets", "images", tipo, nomeArquivo);

                // Verificar se o arquivo existe
                if (!System.IO.File.Exists(caminhoImagem))
                {
                    _logger.LogWarning("Imagem não encontrada: {Caminho}", caminhoImagem);
                    return NotFound("Imagem não encontrada");
                }

                // Determinar o tipo MIME baseado na extensão
                var extensao = Path.GetExtension(nomeArquivo).ToLowerInvariant();
                var tipoMime = extensao switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    ".webp" => "image/webp",
                    _ => "application/octet-stream"
                };

                // Ler e retornar a imagem
                var bytes = System.IO.File.ReadAllBytes(caminhoImagem);
                return File(bytes, tipoMime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao servir imagem: {Tipo}/{NomeArquivo}", tipo, nomeArquivo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Serve uma imagem de usuário
        /// </summary>
        [HttpGet("usuarios/{nomeArquivo}")]
        public IActionResult GetImagemUsuario(string nomeArquivo)
        {
            return GetImagem("usuarios", nomeArquivo);
        }

        /// <summary>
        /// Serve uma imagem de projeto
        /// </summary>
        [HttpGet("projetos/{nomeArquivo}")]
        public IActionResult GetImagemProjeto(string nomeArquivo)
        {
            return GetImagem("projetos", nomeArquivo);
        }

        /// <summary>
        /// Serve uma imagem de portfolio
        /// </summary>
        [HttpGet("portfolios/{nomeArquivo}")]
        public IActionResult GetImagemPortfolio(string nomeArquivo)
        {
            return GetImagem("portfolios", nomeArquivo);
        }

        /// <summary>
        /// Serve uma imagem padrão quando a imagem não for encontrada
        /// </summary>
        [HttpGet("default/{tipo}")]
        public IActionResult GetImagemPadrao(string tipo)
        {
            try
            {
                var nomeArquivoPadrao = tipo switch
                {
                    "usuarios" => "default-user.png",
                    "projetos" => "default-project.png",
                    "portfolios" => "default-portfolio.png",
                    _ => "default.png"
                };

                var caminhoImagemPadrao = Path.Combine(_environment.WebRootPath, "assets", "images", "default", nomeArquivoPadrao);

                if (!System.IO.File.Exists(caminhoImagemPadrao))
                {
                    // Retornar uma imagem vazia ou placeholder
                    return NotFound("Imagem padrão não encontrada");
                }

                var extensao = Path.GetExtension(nomeArquivoPadrao).ToLowerInvariant();
                var tipoMime = extensao switch
                {
                    ".jpg" or ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".gif" => "image/gif",
                    ".bmp" => "image/bmp",
                    ".webp" => "image/webp",
                    _ => "application/octet-stream"
                };

                var bytes = System.IO.File.ReadAllBytes(caminhoImagemPadrao);
                return File(bytes, tipoMime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao servir imagem padrão para tipo: {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 