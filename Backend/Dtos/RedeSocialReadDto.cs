namespace TrabukaApi.Dtos
{
    public class RedeSocialReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Plataforma { get; set; }
        public string URL { get; set; }
        public string Icone { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 