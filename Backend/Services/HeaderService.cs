using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models.Enums;
using TrabukaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TrabukaApi.Services
{
    public class HeaderService : IHeaderService
    {
        private readonly TrabukaDbContext _context;
        private readonly ILogger<HeaderService> _logger;

        public HeaderService(TrabukaDbContext context, ILogger<HeaderService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HeaderJovemDto> GetHeaderJovemAsync(int usuarioId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == usuarioId && u.TipoUsuario == TipoUsuario.Jovem);
            if (usuario == null) return null;

            // Saudação
            var saudacao = $"Bem-vindo de volta, {usuario.Nome.Split(' ')[0]}!";
            var nivelNome = ObterNomeNivel(usuario.Nivel ?? NivelUsuario.Explorador);
            var badgeNivel = nivelNome.ToLower();
            var fraseMotivacional = "Pronto para impulsionar sua carreira?";

            // Corrigir caminho da foto de perfil
            var fotoPerfil = usuario.FotoPerfil;
            if (!string.IsNullOrEmpty(fotoPerfil))
            {
                if (fotoPerfil.Contains("src/"))
                    fotoPerfil = Path.GetFileName(fotoPerfil);
                fotoPerfil = $"/assets/images/usuarios/{fotoPerfil}";
            }
            else
            {
                fotoPerfil = "/assets/images/default/default-user.png";
            }

            // Notificações não lidas
            var notificacoesNaoLidas = await _context.Notificacoes.CountAsync(n => n.UsuarioId == usuarioId && !n.Lida);

            // Mensagens e fórum (mock)
            var mensagensNaoLidas = 5;
            var forumNaoLidas = 2;

            return new HeaderJovemDto
            {
                UsuarioId = usuario.UsuarioId,
                Nome = usuario.Nome,
                Saudacao = saudacao,
                NivelNome = nivelNome,
                BadgeNivel = badgeNivel,
                FotoPerfil = fotoPerfil,
                NotificacoesNaoLidas = notificacoesNaoLidas,
                MensagensNaoLidas = mensagensNaoLidas,
                ForumNaoLidas = forumNaoLidas,
                FraseMotivacional = fraseMotivacional
            };
        }

        private string ObterNomeNivel(NivelUsuario nivel)
        {
            return nivel switch
            {
                NivelUsuario.Explorador => "Explorador",
                NivelUsuario.Praticante => "Praticante",
                NivelUsuario.Construtor => "Construtor",
                NivelUsuario.Mestre => "Mestre",
                _ => "Explorador"
            };
        }
    }
} 