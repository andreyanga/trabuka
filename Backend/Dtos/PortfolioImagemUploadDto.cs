using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class PortfolioImagemUploadDto
    {
        [Required]
        public IFormFile Imagem { get; set; }
        [Required]
        public int Posicao { get; set; } = 1;
    }
} 