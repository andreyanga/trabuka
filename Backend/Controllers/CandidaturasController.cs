using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Dtos;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidaturasController : ControllerBase
    {
        private readonly TrabukaDbContext _context;

        public CandidaturasController(TrabukaDbContext context)
        {
            _context = context;
        }

        // Jovem cria candidatura
        [HttpPost]
        public async Task<ActionResult<CandidaturaReadDto>> Criar(CandidaturaCreateDto dto)
        {
            var jaExiste = await _context.Candidaturas
                .AnyAsync(c => c.UsuarioId == dto.UsuarioId &&
                               c.ProjetoId == dto.ProjetoId &&
                               c.Status == StatusCandidatura.Pendente);

            if (jaExiste)
                return Conflict("Já existe uma candidatura pendente para este projeto.");

            var candidatura = new Candidatura
            {
                UsuarioId = dto.UsuarioId,
                ProjetoId = dto.ProjetoId,
                DataCandidatura = DateTime.Now,
                Status = StatusCandidatura.Pendente
            };

            _context.Candidaturas.Add(candidatura);
            await _context.SaveChangesAsync();

            return Ok(await MapToReadDto(candidatura));
        }

        // Gestor vê candidaturas pendentes
        [HttpGet("pendentes")]
        public async Task<ActionResult<IEnumerable<CandidaturaReadDto>>> GetPendentes()
        {
            var pendentes = await _context.Candidaturas
                .Include(c => c.Usuario)
                .Include(c => c.Projeto)
                .Where(c => c.Status == StatusCandidatura.Pendente)
                .ToListAsync();

            var dtos = new List<CandidaturaReadDto>();
            foreach (var c in pendentes)
            {
                dtos.Add(await MapToReadDto(c));
            }

            return Ok(dtos);
        }

        // Gestor aprova candidatura
        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(int id)
        {
            var candidatura = await _context.Candidaturas
                .Include(c => c.Usuario)
                .Include(c => c.Projeto)
                .FirstOrDefaultAsync(c => c.CandidaturaId == id);

            if (candidatura == null) return NotFound();

            candidatura.Status = StatusCandidatura.Aprovada;

            _context.Candidaturas.Update(candidatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Gestor rejeita candidatura
        [HttpPut("{id}/rejeitar")]
        public async Task<IActionResult> Rejeitar(int id)
        {
            var candidatura = await _context.Candidaturas.FindAsync(id);
            if (candidatura == null) return NotFound();

            candidatura.Status = StatusCandidatura.Rejeitada;

            _context.Candidaturas.Update(candidatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<CandidaturaReadDto> MapToReadDto(Candidatura c)
        {
            if (c.Usuario == null)
                c.Usuario = await _context.Usuarios.FindAsync(c.UsuarioId) ?? new Usuario { Nome = "N/A" };
            if (c.Projeto == null)
                c.Projeto = await _context.Projetos.FindAsync(c.ProjetoId) ?? new Projeto { Descricao = "N/A" };

            return new CandidaturaReadDto
            {
                Id = c.CandidaturaId,
                UsuarioId = c.UsuarioId,
                NomeUsuario = c.Usuario.Nome,
                ProjetoId = c.ProjetoId,
                DescricaoProjeto = c.Projeto.Descricao,
                DataCandidatura = c.DataCandidatura,
                Status = c.Status
            };
        }
    }
}
