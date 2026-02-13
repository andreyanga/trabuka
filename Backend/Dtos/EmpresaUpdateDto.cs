using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class EmpresaUpdateDto
    {
        [Required]
        public string Nome { get; set; }
        public string Setor { get; set; }
        public string Contato { get; set; }
    }
} 