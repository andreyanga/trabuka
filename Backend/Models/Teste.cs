using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Teste
    {
        [Key]
        public int TesteId { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DificuldadeTeste Dificuldade { get; set; }
        
        public int PontuacaoMaxima { get; set; } // Pontuação total do teste
        
        public int PontuacaoMinima { get; set; } = 70; // Pontuação mínima para aprovação (padrão 70/100)
        
        public int TempoLimiteMinutos { get; set; } = 60; // Tempo limite em minutos (padrão 60)

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        // Navegação
        public ICollection<ResultadoTeste> ResultadosTeste { get; set; }
        public ICollection<Questao> Questoes { get; set; }
    }
} 