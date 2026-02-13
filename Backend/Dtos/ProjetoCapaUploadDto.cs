using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class ProjetoCapaUploadDto
    {
        [Required]
        public IFormFile Imagem { get; set; }
    }
} 