using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Certificacao
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; } = null!;

        public string Nome { get; set; } = null!;
        public int Ano { get; set; }
        public DateTime created_at { get; set; }
    }
} 