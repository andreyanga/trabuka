using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class TesteUpdateDto
    {
        [Required]
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public DificuldadeTeste Dificuldade { get; set; }
        public int PontuacaoMaxima { get; set; }
        public int PontuacaoMinima { get; set; }
        public int TempoLimiteMinutos { get; set; }
        public bool Ativo { get; set; }
    }
} 