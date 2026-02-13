using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class PagamentoCreateDto
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public int Status { get; set; }
    }
} 