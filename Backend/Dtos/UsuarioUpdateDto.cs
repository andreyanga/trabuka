using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioUpdateDto
    {
        public string? Nome { get; set; }
        public string? CV { get; set; }
        public string? Habilidades { get; set; }
        public string? FotoPerfil { get; set; }
        public string? Telefone { get; set; }
        public NivelUsuario? Nivel { get; set; }
        public StatusUsuario? Status { get; set; }
    }
} 