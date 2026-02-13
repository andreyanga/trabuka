using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Projeto
    {
        [Key]
        public int ProjetoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string Tipo { get; set; }
        public int Oramento { get; set; }
        public DateTime Horario { get; set; }
        public DateTime Prazo { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public int? MentorId { get; set; }
        public Usuario Mentor { get; set; }

        public StatusProjeto Status { get; set; }
        public string ImagemCapa { get; set; }

        // Navegação
        public ICollection<Relatorio> Relatorios { get; set; }
        public ICollection<Equipe> Equipes { get; set; }
    }
} 