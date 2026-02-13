using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SenhaHash { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public TipoUsuario TipoUsuario { get; set; }

        public NivelUsuario? Nivel { get; set; }

        // Status de aprovação/uso na plataforma (Pendente, Ativo, etc.)
        public StatusUsuario Status { get; set; } = StatusUsuario.Pendente;

        public string CV { get; set; }
        public string Habilidades { get; set; }
        public string FotoPerfil { get; set; }

        // Navegação
        public ICollection<Pagamento> Pagamentos { get; set; }
        public ICollection<Notificacao> Notificacoes { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<ResultadoTeste> ResultadosTeste { get; set; }
        public ICollection<Relatorio> Relatorios { get; set; }
        public Portfolio Portfolio { get; set; }
        public ICollection<Sessao> Sessoes { get; set; }
        public ICollection<Log> Logs { get; set; }
        public ICollection<Configuracao> Configuracoes { get; set; }
        public ICollection<UsuarioEquipe> UsuarioEquipes { get; set; }
    }
} 