namespace TrabukaApi.Dtos
{
    public class FuncionalidadeCreateDto
    {
        public int PortfolioId { get; set; }
        public string Tipo { get; set; }
        public int ReferenciaId { get; set; }
        public string Titulo { get; set; }
        public string Icone { get; set; }
        public string Descricao { get; set; }
    }
} 