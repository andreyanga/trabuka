using System;
namespace TrabukaApi.Dtos
{
    public class MetricaCreateDto
    {
        public TrabukaApi.Models.Enums.TipoMetrica Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
    }
} 