using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Relatorio
    {
        [Key]
        public int id_relatorio { get; set; }

        public int id_projeto { get; set; }
        public Projeto Projeto { get; set; } = null!;

        public int id_usuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public DateTime data_envio { get; set; }
        public string evidencias { get; set; } = null!;
        public string descricao { get; set; } = null!;
        public string feedback { get; set; } = null!;
        public StatusRelatorio status { get; set; }
    }
} 