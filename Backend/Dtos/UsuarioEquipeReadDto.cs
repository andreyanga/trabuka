using System;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioEquipeReadDto
    {
        public int UsuarioId { get; set; }
        public int EquipeId { get; set; }
        public PapelEquipe Papel { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 