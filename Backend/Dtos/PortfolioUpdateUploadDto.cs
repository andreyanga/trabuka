using Microsoft.AspNetCore.Http;
using System;

namespace TrabukaApi.Dtos
{
    public class PortfolioUpdateUploadDto
    {
        public string URL { get; set; }
        public DateTime DataConclusao { get; set; }
        public IFormFile Imagem1 { get; set; }
        public IFormFile Imagem2 { get; set; }
    }
} 