using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class UsuarioUpdateUploadDto
    {
        public string? Nome { get; set; }
        public string? CV { get; set; }
        public string? Habilidades { get; set; }
        public string? Telefone { get; set; }
        public IFormFile? FotoPerfil { get; set; }
        public TrabukaApi.Models.Enums.NivelUsuario? Nivel { get; set; }
    }
} 