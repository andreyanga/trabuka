using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class RelatorioCreateDto
    {
        [Required]
        public int ProjetoId { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public DateTime DataEnvio { get; set; }
        public string Evidencias { get; set; }
        public string Descricao { get; set; }
        public string Feedback { get; set; }
        public int Status { get; set; }
    }
} 