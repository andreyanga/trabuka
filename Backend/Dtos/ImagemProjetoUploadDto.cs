using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class ImagemProjetoUploadDto
    {
        [Required]
        public int PortfolioId { get; set; }
        [Required]
        public IFormFile Imagem { get; set; }
        public string Descricao { get; set; }
    }
} 