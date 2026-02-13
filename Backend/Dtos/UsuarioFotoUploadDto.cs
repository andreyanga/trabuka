using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class UsuarioFotoUploadDto
    {
        [Required]
        public IFormFile Foto { get; set; }
    }
} 