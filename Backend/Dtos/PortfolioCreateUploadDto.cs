using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class PortfolioCreateUploadDto
    {
        [Required]
        public int UsuarioId { get; set; }
        public string URL { get; set; }
        public DateTime DataConclusao { get; set; }
        public IFormFile Imagem1 { get; set; }
        public IFormFile Imagem2 { get; set; }
    }
} 