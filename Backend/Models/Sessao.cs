using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Sessao
    {
        [Key]
        public int id { get; set; }

        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; }

        public string token { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_expiracao { get; set; }
        public string dispositivo { get; set; }
        public StatusSessao status { get; set; }
        public DateTime created_at { get; set; }
    }
} 