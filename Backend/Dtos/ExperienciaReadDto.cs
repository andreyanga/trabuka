namespace TrabukaApi.Dtos
{
    public class ExperienciaReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public string Conquistas { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 