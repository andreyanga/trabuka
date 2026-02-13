namespace TrabukaApi.Models
{
    public class ServicosRelacionados
    {
        public int servico_id { get; set; }
        public Servico Servico { get; set; }

        public int relacionado_id { get; set; }
        public Servico ServicoRelacionado { get; set; }
    }
} 