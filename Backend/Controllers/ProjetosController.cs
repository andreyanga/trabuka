using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class ProjetosController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IEmpresaService _empresaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<ProjetosController> _logger;

        public ProjetosController(IProjetoService projetoService, IEmpresaService empresaService, IUsuarioService usuarioService, ILogger<ProjetosController> logger)
        {
            _projetoService = projetoService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os projetos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetos()
        {
            try
            {
                var projetos = await _projetoService.GetAllAsync();
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém um projeto por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoReadDto>> GetProjeto(int id)
        {
            try
            {
                var projeto = await _projetoService.GetByIdAsync(id);
                if (projeto == null)
                {
                    return NotFound($"Projeto com ID {id} não encontrado");
                }
                return Ok(projeto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projeto com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um novo projeto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProjetoReadDto>> CreateProjeto([FromForm] ProjetoCreateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string imagemCapaPath = null;
                if (dto.ImagemCapa != null && dto.ImagemCapa.Length > 0)
                {
                    imagemCapaPath = await Helpers.FileUploadHelper.SaveImageAsync(dto.ImagemCapa, "projetos");
                }

                var projetoDto = new ProjetoCreateDto
                {
                    Descricao = dto.Descricao,
                    Tipo = dto.Tipo,
                    Oramento = dto.Oramento,
                    Horario = dto.Horario,
                    Prazo = dto.Prazo,
                    EmpresaId = dto.EmpresaId,
                    MentorId = dto.MentorId,
                    Status = dto.Status,
                    ImagemCapa = imagemCapaPath
                };

                var projeto = await _projetoService.CreateAsync(projetoDto);
                return CreatedAtAction(nameof(GetProjeto), new { id = projeto.Id }, projeto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar projeto");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Cria um projeto recebendo JSON (sem upload de imagem). Se o EmpresaId enviado não existir,
        /// tenta criar uma empresa a partir do usuário (quando aplicável).
        /// </summary>
        [HttpPost("create-json")]
        public async Task<ActionResult<ProjetoReadDto>> CreateProjetoJson([FromBody] ProjetoCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verifica se a empresa existe
                bool empresaExists = true;
                try
                {
                    var empresa = await _empresaService.GetByIdAsync(dto.EmpresaId);
                    empresaExists = empresa != null;
                }
                catch
                {
                    empresaExists = false;
                }

                if (!empresaExists)
                {
                    // Tenta localizar um usuário com o id fornecido
                    var usuario = await _usuarioService.GetByIdAsync(dto.EmpresaId);
                    if (usuario == null)
                    {
                        return BadRequest("Empresa não encontrada. Cadastre o perfil da empresa antes de publicar um projeto.");
                    }

                    // Tenta encontrar uma empresa cujo email coincide com o email do usuário
                    var empresas = await _empresaService.GetAllAsync();
                    var empresaPorEmail = empresas.FirstOrDefault(e => string.Equals(e.Email?.Trim(), usuario.Email?.Trim(), StringComparison.OrdinalIgnoreCase));
                    if (empresaPorEmail != null)
                    {
                        dto.EmpresaId = empresaPorEmail.Id;
                    }
                    else
                    {
                        // Se o usuário for do tipo Empresa, cria a empresa a partir do usuário
                        if (usuario.TipoUsuario == TrabukaApi.Models.Enums.TipoUsuario.Empresa || usuario.TipoUsuario.ToString().ToLower().Contains("empresa"))
                        {
                            var novaEmpresaDto = new Dtos.EmpresaCreateDto
                            {
                                Nome = usuario.Nome,
                                Email = usuario.Email,
                                Contato = usuario.Telefone ?? ""
                            };
                            var criada = await _empresaService.CreateAsync(novaEmpresaDto);
                            dto.EmpresaId = criada.Id;
                        }
                        else
                        {
                            return BadRequest("Empresa não encontrada. Cadastre o perfil da empresa antes de publicar um projeto.");
                        }
                    }
                }

                var projeto = await _projetoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetProjeto), new { id = projeto.Id }, projeto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar projeto via JSON");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um projeto existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjeto(int id, ProjetoUpdateDto projetoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var projeto = await _projetoService.UpdateAsync(id, projetoDto);
                if (projeto == null)
                {
                    return NotFound($"Projeto com ID {id} não encontrado");
                }

                return Ok(projeto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar projeto com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Atualiza um projeto existente (com upload de imagem de capa)
        /// </summary>
        [HttpPut("{id}/upload")]
        public async Task<IActionResult> UpdateProjetoComUpload(int id, [FromForm] ProjetoUpdateUploadDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string imagemCapaPath = null;
                if (dto.ImagemCapa != null && dto.ImagemCapa.Length > 0)
                {
                    imagemCapaPath = await Helpers.FileUploadHelper.SaveImageAsync(dto.ImagemCapa, "projetos");
                }

                var projetoUpdateDto = new ProjetoUpdateDto
                {
                    Descricao = dto.Descricao,
                    Tipo = dto.Tipo,
                    Oramento = dto.Oramento,
                    Horario = dto.Horario,
                    Prazo = dto.Prazo,
                    MentorId = dto.MentorId,
                    Status = dto.Status,
                    ImagemCapa = imagemCapaPath
                };

                var projeto = await _projetoService.UpdateAsync(id, projetoUpdateDto);
                if (projeto == null)
                {
                    return NotFound($"Projeto com ID {id} não encontrado");
                }

                return Ok(projeto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar projeto com upload");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Remove um projeto
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjeto(int id)
        {
            try
            {
                var result = await _projetoService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound($"Projeto com ID {id} não encontrado");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar projeto com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos por empresa
        /// </summary>
        [HttpGet("empresa/{empresaId}")]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetosPorEmpresa(int empresaId)
        {
            try
            {
                var projetos = await _projetoService.GetByEmpresaAsync(empresaId);
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos da empresa {EmpresaId}", empresaId);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos por status
        /// </summary>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetosPorStatus(StatusProjeto status)
        {
            try
            {
                var projetos = await _projetoService.GetByStatusAsync(status);
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos por status {Status}", status);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Aprova um projeto (altera status para Ativo)
        /// </summary>
        [HttpPatch("{id}/aprovar")]
        public async Task<IActionResult> AprovarProjeto(int id)
        {
            try
            {
                var projetoExistente = await _projetoService.GetByIdAsync(id);
                if (projetoExistente == null)
                {
                    return NotFound($"Projeto com ID {id} não encontrado");
                }

                var updateDto = new ProjetoUpdateDto
                {
                    Descricao = projetoExistente.Descricao,
                    Tipo = projetoExistente.Tipo,
                    Oramento = projetoExistente.Oramento,
                    Horario = projetoExistente.Horario,
                    Prazo = projetoExistente.Prazo,
                    MentorId = projetoExistente.MentorId,
                    Status = (int)StatusProjeto.Ativo,
                    ImagemCapa = projetoExistente.ImagemCapa
                };

                var atualizado = await _projetoService.UpdateAsync(id, updateDto);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao aprovar projeto com ID {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos por tipo
        /// </summary>
        [HttpGet("tipo/{tipo}")]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetosPorTipo(TipoProjeto tipo)
        {
            try
            {
                var projetos = await _projetoService.GetByTipoAsync(tipo);
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos por tipo {Tipo}", tipo);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos por localização
        /// </summary>
        [HttpGet("localizacao/{provincia}")]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetosPorLocalizacao(string provincia)
        {
            try
            {
                var projetos = await _projetoService.GetByLocalizacaoAsync(provincia);
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos por localização {Provincia}", provincia);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Obtém projetos por faixa salarial
        /// </summary>
        [HttpGet("salario/{minSalario}/{maxSalario}")]
        public async Task<ActionResult<IEnumerable<ProjetoReadDto>>> GetProjetosPorFaixaSalarial(decimal minSalario, decimal maxSalario)
        {
            try
            {
                var projetos = await _projetoService.GetByFaixaSalarialAsync(minSalario, maxSalario);
                return Ok(projetos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar projetos por faixa salarial {MinSalario}-{MaxSalario}", minSalario, maxSalario);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Faz upload da imagem de capa do projeto
        /// </summary>
        [HttpPost("{id}/upload-capa")]
        public async Task<IActionResult> UploadImagemCapa(int id, [FromForm] ProjetoCapaUploadDto dto)
        {
            try
            {
                if (dto.Imagem == null || dto.Imagem.Length == 0)
                    return BadRequest("Arquivo de imagem inválido.");

                var caminho = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem, "projetos");
                var projeto = await _projetoService.GetByIdAsync(id);
                if (projeto == null)
                    return NotFound($"Projeto com ID {id} não encontrado");

                // Atualiza o campo ImagemCapa
                await _projetoService.AtualizarImagemCapaAsync(id, caminho);
                return Ok(new { imagemCapa = caminho });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da imagem de capa do projeto {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Faz upload de uma imagem para o projeto (ImagemProjeto)
        /// </summary>
        [HttpPost("{id}/upload-imagem")]
        public async Task<IActionResult> UploadImagemProjeto(int id, [FromForm] ProjetoImagemUploadDto dto)
        {
            try
            {
                if (dto.Imagem == null || dto.Imagem.Length == 0)
                    return BadRequest("Arquivo de imagem inválido.");

                var caminho = await Helpers.FileUploadHelper.SaveImageAsync(dto.Imagem, "projetos");
                // Aqui você pode adicionar lógica para registrar a imagem no banco, se desejar
                return Ok(new { imagemProjeto = caminho });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da imagem do projeto {Id}", id);
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
} 