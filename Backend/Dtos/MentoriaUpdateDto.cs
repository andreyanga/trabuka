using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class MentoriaUpdateDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Feedback { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 