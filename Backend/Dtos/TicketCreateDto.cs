using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class TicketCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
} 