namespace TrabukaApi.Dtos
{
    public class ConfiguracaoReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Chave { get; set; }
        public string Valor { get; set; }
        public string Descricao { get; set; }
    }
} 