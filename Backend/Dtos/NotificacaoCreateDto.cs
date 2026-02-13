using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class NotificacaoCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string Mensagem { get; set; }
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
    }
} 