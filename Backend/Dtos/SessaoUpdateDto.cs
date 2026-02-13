using System;
namespace TrabukaApi.Dtos
{
    public class SessaoUpdateDto
    {
        public DateTime DataExpiracao { get; set; }
        public string Dispositivo { get; set; }
        public TrabukaApi.Models.Enums.StatusSessao Status { get; set; }
    }
} 