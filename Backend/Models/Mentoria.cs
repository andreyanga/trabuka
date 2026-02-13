using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Mentoria
    {
        [Key]
        public int id { get; set; }

        public int mentor_id { get; set; }
        public Usuario Mentor { get; set; }

        public int mentorado_id { get; set; }
        public Usuario Mentorado { get; set; }

        public int projeto_id { get; set; }
        public Projeto Projeto { get; set; }

        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public string feedback { get; set; }
        public DateTime created_at { get; set; }
    }
} 