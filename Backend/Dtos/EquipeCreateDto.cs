using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class EquipeCreateDto
    {
        [Required]
        public int ProjetoId { get; set; }
        [Required]
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
    }
} 