using System;

namespace TrabukaApi.Dtos
{
    public class FAQReadDto
    {
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
} 