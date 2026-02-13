using Microsoft.AspNetCore.Mvc;
using TrabukaApi.Dtos;
using TrabukaApi.Models;
using TrabukaApi.Services;
using System.Linq;

namespace TrabukaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly Data.TrabukaDbContext _context;

        public AuthController(JwtService jwtService, Data.TrabukaDbContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            string HashPassword(string senha)
            {
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(hashedBytes);
            }

            var senhaHash = HashPassword(dto.Senha);
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email && u.SenhaHash == senhaHash);
            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos");

            var token = _jwtService.GenerateToken(usuario);
            return Ok(new {
                token,
                id = usuario.UsuarioId,
                usuarioId = usuario.UsuarioId,
                nome = usuario.Nome,
                email = usuario.Email,
                tipoUsuario = usuario.TipoUsuario,
                nivel = usuario.Nivel,
                cv = usuario.CV,
                habilidades = usuario.Habilidades,
                fotoPerfil = usuario.FotoPerfil,
                telefone = usuario.Telefone
            });
        }
    }
} 