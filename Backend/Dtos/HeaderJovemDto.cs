namespace TrabukaApi.Dtos
{
    public class HeaderJovemDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Saudacao { get; set; }
        public string NivelNome { get; set; }
        public string BadgeNivel { get; set; }
        public string FotoPerfil { get; set; }
        public int NotificacoesNaoLidas { get; set; }
        public int MensagensNaoLidas { get; set; }
        public int ForumNaoLidas { get; set; }
        public string FraseMotivacional { get; set; }
    }
} 