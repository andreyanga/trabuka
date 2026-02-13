using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class EmpresaCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Setor { get; set; }
        public string Contato { get; set; }
    }
} 