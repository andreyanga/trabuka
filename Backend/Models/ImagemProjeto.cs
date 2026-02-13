using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class ImagemProjeto
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public string imagem_url { get; set; }
        public string Descricao { get; set; }
        public DateTime created_at { get; set; }
    }
} 