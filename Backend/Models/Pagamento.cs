using System;
using System.ComponentModel.DataAnnotations;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Models
{
    public class Pagamento
    {
        [Key]
        public int PagamentoId { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public StatusPagamento Status { get; set; }
    }
} 