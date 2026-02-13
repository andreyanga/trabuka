using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class PortfolioUpdateDto
    {
        public string URL { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Imagem1 { get; set; }
        public string Imagem2 { get; set; }
    }
} 