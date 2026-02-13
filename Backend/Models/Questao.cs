using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabukaApi.Models
{
    public class Questao
    {
        [Key]
        public int QuestaoId { get; set; }

        [Required]
        public int TesteId { get; set; }

        [ForeignKey("TesteId")]
        public Teste Teste { get; set; }

        [Required]
        public string Enunciado { get; set; }

        [Required]
        public string OpcaoA { get; set; }

        [Required]
        public string OpcaoB { get; set; }

        [Required]
        public string OpcaoC { get; set; }

        [Required]
        public string OpcaoD { get; set; }

        [Required]
        [MaxLength(1)]
        public string RespostaCorreta { get; set; } // A, B, C ou D

        public int Pontuacao { get; set; } = 1; // Pontos por questão

        public int Ordem { get; set; } // Ordem da questão no teste

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
