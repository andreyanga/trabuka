using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Servico
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;

        public string Titulo { get; set; } = null!;
        public string Icone { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public string Duracao { get; set; } = null!;
        public string Gerente { get; set; } = null!;
        public string ContatoSuporte { get; set; } = null!;
        public DateTime created_at { get; set; }
    }
} 