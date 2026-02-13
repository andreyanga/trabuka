using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class ResultadoTeste
    {
        [Key]
        public int id_resultado { get; set; }

        public int id_teste { get; set; }
        public Teste Teste { get; set; } = null!;

        public int id_usuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int pontuacao { get; set; }
        public NivelUsuario? nivel_atribuido { get; set; }
        public DateTime data_conclusao { get; set; }
    }
} 