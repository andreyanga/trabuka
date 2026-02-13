using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Configuracao
    {
        [Key]
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public string chave { get; set; } = null!;
        public string valor { get; set; } = null!;
        public string descricao { get; set; } = null!;
        public DateTime created_at { get; set; }
    }
} 