using System;
using System.Collections.Generic;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class TesteComQuestoesDto
    {
        public int TesteId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DificuldadeTeste Dificuldade { get; set; }
        public int TempoLimiteMinutos { get; set; }
        public int PontuacaoMaxima { get; set; }
        public int PontuacaoMinima { get; set; }
        public List<QuestaoReadDto> Questoes { get; set; }
        public DateTime DataInicio { get; set; } // Quando o usuário iniciou o teste
    }
}
