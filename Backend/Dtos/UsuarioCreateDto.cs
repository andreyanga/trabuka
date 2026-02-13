using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
        [Required]
        public string Telefone { get; set; }
        public string? CV { get; set; }
        public string? Habilidades { get; set; }
        public string FotoPerfil { get; set; }
    }
} 