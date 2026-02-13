using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class PagamentoUpdateDto
    {
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public int Status { get; set; }
    }
} 