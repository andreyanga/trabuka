using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Metrica
    {
        [Key]
        public int id { get; set; }
        public TipoMetrica tipo { get; set; }
        public decimal valor { get; set; }
        public DateTime data { get; set; }
        public string descricao { get; set; }
        public DateTime created_at { get; set; }
    }
} 