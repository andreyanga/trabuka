using System;

namespace TrabukaApi.Dtos
{
    public class PortfolioReadDto
    {
        public int PortfolioId { get; set; }
        public int UsuarioId { get; set; }
        public string URL { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Imagem1 { get; set; }
        public string Imagem2 { get; set; }
        public int Id { get; set; }
    }
} 