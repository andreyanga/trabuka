using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class AvaliarRelatorioDto
    {
        [Range(0, 10)]
        public int Nota { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}

