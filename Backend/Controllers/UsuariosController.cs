using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioService usuarioService, ILogger<UsuariosController> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um usuário por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsuarioReadDto>> CreateUsuario([FromForm] UsuarioCreateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string fotoPerfilPath = null;
                if (dto.FotoPerfil != null && dto.FotoPerfil.Length > 0)
                {
                    fotoPerfilPath = await Helpers.FileUploadHelper.SaveImageAsync(dto.FotoPerfil, "usuarios");
                }

                var usuarioDto = new UsuarioCreateDto
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    TipoUsuario = dto.TipoUsuario,
                    CV = dto.CV,
                    Habilidades = dto.Habilidades,
                    FotoPerfil = fotoPerfilPath,
                    Telefone = dto.Telefone
                };

                var usuario = await _usuarioService.CreateAsync(usuarioDto);
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar usuário");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioUpdateDto usuarioDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var usuario = await _usuarioService.UpdateAsync(id, usuarioDto);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }

                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um usuário existente (com upload de foto de perfil)
        /// </summary>
        [HttpPut("{id}/upload")]
        public async Task<IActionResult> UpdateUsuarioComUpload(int id, [FromForm] UsuarioUpdateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string fotoPerfilPath = null;
                if (dto.FotoPerfil != null && dto.FotoPerfil.Length > 0)
                {
                    fotoPerfilPath = await Helpers.FileUploadHelper.SaveImageAsync(dto.FotoPerfil, "usuarios");
                }

                var usuarioUpdateDto = new UsuarioUpdateDto
                {
                    Nome = dto.Nome,
                    CV = dto.CV,
                    Habilidades = dto.Habilidades,
                    FotoPerfil = fotoPerfilPath,
                    Nivel = dto.Nivel,
                    Telefone = dto.Telefone
                };

                var usuario = await _usuarioService.UpdateAsync(id, usuarioUpdateDto);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }

                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar usuário com upload");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var result = await _usuarioService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar usuário com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém usuários por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetUsuariosPorTipo(TipoUsuario tipo)
        {
            try
            {
                var usuarios = await _usuarioService.GetByTipoAsync(tipo);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém usuários por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> GetUsuariosPorStatus(StatusUsuario status)
        {
            try
            {
                var usuarios = await _usuarioService.GetByStatusAsync(status);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuários por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Aprova um usuário (altera status para Ativo)
        /// </summary>
        [HttpPatch("{id}/aprovar")]
        public async Task<IActionResult> AprovarUsuario(int id)
        {
            try
            {
                var updateDto = new UsuarioUpdateDto
                {
                    Status = StatusUsuario.Ativo
                };

                var usuario = await _usuarioService.UpdateAsync(id, updateDto);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao aprovar usuário com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Faz upload da foto de perfil do usuário
        /// </summary>
        [HttpPost("{id}/upload-foto")]
        public async Task<IActionResult> UploadFotoPerfil(int id, [FromForm] UsuarioFotoUploadDto dto)
        {
            try
            {
                if (dto.Foto == null || dto.Foto.Length == 0)
                    return BadRequest("Arquivo de imagem inválido.");

                var caminho = await Helpers.FileUploadHelper.SaveImageAsync(dto.Foto, "usuarios");
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                    return NotFound($"Usuário com ID {id} não encontrado");

                // Atualiza o campo FotoPerfil
                await _usuarioService.AtualizarFotoPerfilAsync(id, caminho);
                return Ok(new { fotoPerfil = caminho });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da foto de perfil do usuário {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 