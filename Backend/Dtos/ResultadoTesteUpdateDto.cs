using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class ResultadoTesteUpdateDto
    {
        public int Pontuacao { get; set; }
        public NivelUsuario? NivelAtribuido { get; set; }
        public DateTime DataConclusao { get; set; }
    }
} 