namespace TrabukaApi.Dtos
{
    public class ServicosRelacionadosReadDto
    {
        public int ServicoId { get; set; }
        public int RelacionadoId { get; set; }
        public string ServicoNome { get; set; }
        public string RelacionadoNome { get; set; }
        public string ServicoTitulo { get; set; }
        public string RelacionadoTitulo { get; set; }
    }
} 