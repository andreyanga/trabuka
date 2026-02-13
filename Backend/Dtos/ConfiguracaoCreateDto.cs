namespace TrabukaApi.Dtos
{
    public class ConfiguracaoCreateDto
    {
        public int UsuarioId { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
    }
} 