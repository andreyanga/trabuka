using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class UsuarioEquipe
    {
        public int usuario_id { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int equipe_id { get; set; }
        public Equipe Equipe { get; set; } = null!;

        public PapelEquipe papel { get; set; }
        public DateTime data_entrada { get; set; }
        public DateTime created_at { get; set; }
    }
} 