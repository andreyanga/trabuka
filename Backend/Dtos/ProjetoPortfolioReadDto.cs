namespace TrabukaApi.Dtos
{
    public class ProjetoPortfolioReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Categoria { get; set; }
        public string Titulo { get; set; }
        public string Cliente { get; set; }
        public DateTime DataProjeto { get; set; }
        public string UrlProjeto { get; set; }
        public string Descricao { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 