using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Educacao
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;

        public string Curso { get; set; } = null!;
        public string Instituicao { get; set; } = null!;
        public DateTime periodo_inicio { get; set; }
        public DateTime periodo_fim { get; set; }
        public string Descricao { get; set; } = null!;
        public DateTime created_at { get; set; }
    }
} 