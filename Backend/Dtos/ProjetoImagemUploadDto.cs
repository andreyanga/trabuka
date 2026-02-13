using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class ProjetoImagemUploadDto
    {
        [Required]
        public IFormFile Imagem { get; set; }
        public string Descricao { get; set; }
    }
} 