namespace TrabukaApi.Dtos
{
    public class ServicoReadDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string Titulo { get; set; }
    }
} 