using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioEquipeCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int EquipeId { get; set; }
        public PapelEquipe Papel { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 