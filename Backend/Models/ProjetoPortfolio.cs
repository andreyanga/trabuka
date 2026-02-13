using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class ProjetoPortfolio
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public string Categoria { get; set; }
        public string Titulo { get; set; }
        public string Cliente { get; set; }
        public DateTime data_projeto { get; set; }
        public string url_projeto { get; set; }
        public string Descricao { get; set; }
        public DateTime created_at { get; set; }
    }
} 