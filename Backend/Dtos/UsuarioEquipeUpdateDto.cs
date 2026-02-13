using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioEquipeUpdateDto
    {
        public PapelEquipe Papel { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 