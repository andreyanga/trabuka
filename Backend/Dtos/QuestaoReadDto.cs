namespace TrabukaApi.Dtos
{
    public class QuestaoReadDto
    {
        public int QuestaoId { get; set; }
        public int TesteId { get; set; }
        public string Enunciado { get; set; }
        public string OpcaoA { get; set; }
        public string OpcaoB { get; set; }
        public string OpcaoC { get; set; }
        public string OpcaoD { get; set; }
        public int Pontuacao { get; set; }
        public int Ordem { get; set; }
        // Não incluir RespostaCorreta no DTO de leitura para o usuário
    }
}
