using System;

namespace TrabukaApi.Dtos
{
    public class NotificacaoReadDto
    {
        public int NotificacaoId { get; set; }
        public int UsuarioId { get; set; }
        public string Mensagem { get; set; }
        public bool Lida { get; set; }
        public DateTime DataEnvio { get; set; }
        public int Id { get; set; }
    }
} 