using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Setor { get; set; }
        public string Contato { get; set; }

        // Navegação
        public ICollection<Projeto> Projetos { get; set; }
        public ICollection<Pagamento> Pagamentos { get; set; }
    }
} 