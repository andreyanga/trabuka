using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class TicketUpdateDto
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int Status { get; set; }
    }
} 