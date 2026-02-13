using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Experiencia
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;

        public string Cargo { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public DateTime periodo_inicio { get; set; }
        public DateTime periodo_fim { get; set; }
        public string Conquistas { get; set; } = null!;
        public DateTime created_at { get; set; }
    }
} 