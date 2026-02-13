using System;

namespace TrabukaApi.Dtos
{
    public class TicketReadDto
    {
        public int TicketId { get; set; }
        public int UsuarioId { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Id { get; set; }
    }
} 