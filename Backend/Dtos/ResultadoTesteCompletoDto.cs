using System;
using System.Collections.Generic;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class ResultadoTesteCompletoDto
    {
        public int ResultadoId { get; set; }
        public int TesteId { get; set; }
        public string NomeTeste { get; set; }
        public int UsuarioId { get; set; }
        public int PontuacaoObtida { get; set; }
        public int PontuacaoMaxima { get; set; }
        public int PontuacaoMinima { get; set; }
        public bool Aprovado { get; set; }
        public NivelUsuario? NivelAtribuido { get; set; }
        public DateTime DataConclusao { get; set; }
        public List<RespostaDetalhadaDto> RespostasDetalhadas { get; set; }
    }

    public class RespostaDetalhadaDto
    {
        public int QuestaoId { get; set; }
        public string Enunciado { get; set; }
        public string RespostaEscolhida { get; set; }
        public string RespostaCorreta { get; set; }
        public bool Acertou { get; set; }
        public int PontuacaoObtida { get; set; }
    }
}
