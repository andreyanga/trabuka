using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class FAQ
    {
        [Key]
        public int FAQId { get; set; }

        [Required]
        public string Pergunta { get; set; } = null!;

        [Required]
        public string Resposta { get; set; } = null!;

        public DateTime DataPublicacao { get; set; }
    }
} 