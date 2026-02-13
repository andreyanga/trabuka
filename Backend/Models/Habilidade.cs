using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Habilidade
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public string Categoria { get; set; }
        public string Nome { get; set; }
        public int Percentual { get; set; }
        public string Descricao { get; set; }
        public DateTime created_at { get; set; }
    }
} 