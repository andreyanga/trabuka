namespace TrabukaApi.Dtos
{
    public class EducacaoReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Curso { get; set; }
        public string Instituicao { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFim { get; set; }
        public string Descricao { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 