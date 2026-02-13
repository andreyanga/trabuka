using System;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class TesteReadDto
    {
        public int Id { get; set; }
        public int TesteId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DificuldadeTeste Dificuldade { get; set; }
        public int PontuacaoMaxima { get; set; }
        public int PontuacaoMinima { get; set; }
        public int TempoLimiteMinutos { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public int TotalQuestoes { get; set; }
    }
} 