using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Descricao { get; set; }
        public StatusTicket Status { get; set; }
        public DateTime DataCriacao { get; set; }
    }
} 