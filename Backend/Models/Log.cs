using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Log
    {
        [Key]
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; }

        public string acao { get; set; }
        public string detalhes { get; set; }
        public DateTime data_hora { get; set; }
        public string ip { get; set; }
        public DateTime created_at { get; set; }
    }
} 