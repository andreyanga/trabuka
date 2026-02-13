using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class UsuarioReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public NivelUsuario? Nivel { get; set; }
        public string CV { get; set; }
        public string Habilidades { get; set; }
        public string FotoPerfil { get; set; }
        public string Telefone { get; set; }
        public StatusUsuario Status { get; set; }
    }
} 