namespace TrabukaApi.Dtos
{
    public class LogCreateDto
    {
        public int UsuarioId { get; set; }
        public string Acao { get; set; }
        public string Detalhes { get; set; }
        public string Ip { get; set; }
        public System.DateTime DataHora { get; set; }
    }
} 