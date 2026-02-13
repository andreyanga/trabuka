using System;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class ResultadoTesteReadDto
    {
        public int Id { get; set; }
        public int TesteId { get; set; }
        public int UsuarioId { get; set; }
        public int Pontuacao { get; set; }
        public NivelUsuario? NivelAtribuido { get; set; }
        public DateTime DataConclusao { get; set; }
    }
} 