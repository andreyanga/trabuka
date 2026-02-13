using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Funcionalidade
    {
        [Key]
        public int id { get; set; }

        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        public string Tipo { get; set; }
        public int referencia_id { get; set; }
        public string Titulo { get; set; }
        public string Icone { get; set; }
        public string Descricao { get; set; }
        public DateTime created_at { get; set; }
    }
} 