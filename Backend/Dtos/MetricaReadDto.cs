using System;
namespace TrabukaApi.Dtos
{
    public class MetricaReadDto
    {
        public int Id { get; set; }
        public TrabukaApi.Models.Enums.TipoMetrica Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
    }
} 