using System;

namespace TrabukaApi.Dtos
{
    public class MentoriaReadDto
    {
        public int Id { get; set; }
        public int MentorId { get; set; }
        public int MentoradoId { get; set; }
        public int ProjetoId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 