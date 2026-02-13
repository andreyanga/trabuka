using System;
using System.Collections.Generic;

namespace TrabukaApi.Dtos
{
    public class SubmeterTesteDto
    {
        public int TesteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataInicio { get; set; } // Data/hora que iniciou o teste
        public List<RespostaQuestaoDto> Respostas { get; set; }
    }

    public class RespostaQuestaoDto
    {
        public int QuestaoId { get; set; }
        public string RespostaEscolhida { get; set; } // A, B, C ou D
    }
}
