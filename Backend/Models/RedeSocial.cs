using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class RedeSocial
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public string Plataforma { get; set; }
        public string URL { get; set; }
        public string Icone { get; set; }
        public DateTime created_at { get; set; }
    }
} 