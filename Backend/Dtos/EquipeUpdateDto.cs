using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class EquipeUpdateDto
    {
        [Required]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
    }
} 