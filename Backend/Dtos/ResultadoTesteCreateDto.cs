using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class ResultadoTesteCreateDto
    {
        [Required]
        public int TesteId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public int Pontuacao { get; set; }
        public NivelUsuario? NivelAtribuido { get; set; }
        public DateTime DataConclusao { get; set; }
    }
} 