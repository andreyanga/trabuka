using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class NotificacaoUpdateDto
    {
        [Required]
        public string Mensagem { get; set; }
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
    }
} 