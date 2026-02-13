using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class FAQCreateDto
    {
        [Required]
        public string Pergunta { get; set; }
        [Required]
        public string Resposta { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
} 