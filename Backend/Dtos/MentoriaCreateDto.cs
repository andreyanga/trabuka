using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class MentoriaCreateDto
    {
        [Required]
        public int MentorId { get; set; }
        [Required]
        public int MentoradoId { get; set; }
        [Required]
        public int ProjetoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 