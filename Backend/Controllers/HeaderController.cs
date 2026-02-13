using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeaderController : ControllerBase
    {
        private readonly IHeaderService _headerService;
        private readonly ILogger<HeaderController> _logger;

        public HeaderController(IHeaderService headerService, ILogger<HeaderController> logger)
        {
            _headerService = headerService;
            _logger = logger;
        }

        /// <summary>
        /// Header do jovem (usuário tipo 0)
        /// </summary>
        [HttpGet("jovem/{usuarioId}")]
        public async Task<ActionResult<HeaderJovemDto>> GetHeaderJovem(int usuarioId)
        {
            var header = await _headerService.GetHeaderJovemAsync(usuarioId);
            if (header == null)
                return NotFound();
            return Ok(header);
        }
    }
} 