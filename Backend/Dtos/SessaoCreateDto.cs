using System;
namespace TrabukaApi.Dtos
{
    public class SessaoCreateDto
    {
        public int UsuarioId { get; set; }
        public string Token { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataExpiracao { get; set; }
        public string Dispositivo { get; set; }
        public TrabukaApi.Models.Enums.StatusSessao Status { get; set; }
    }
} 