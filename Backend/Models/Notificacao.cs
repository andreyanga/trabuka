using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Notificacao
    {
        [Key]
        public int NotificacaoId { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Mensagem { get; set; }
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
    }
} 