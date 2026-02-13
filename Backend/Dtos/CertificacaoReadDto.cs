namespace TrabukaApi.Dtos
{
    public class CertificacaoReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 