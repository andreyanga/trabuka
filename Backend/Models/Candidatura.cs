using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Candidatura
    {
        [Key]
        public int CandidaturaId { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        [Required]
        public int ProjetoId { get; set; }
        public Projeto Projeto { get; set; } = null!;

        public DateTime DataCandidatura { get; set; } = DateTime.Now;

        public StatusCandidatura Status { get; set; } = StatusCandidatura.Pendente;
    }
}
