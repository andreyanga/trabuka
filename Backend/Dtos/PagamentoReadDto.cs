using System;

namespace TrabukaApi.Dtos
{
    public class PagamentoReadDto
    {
        public int PagamentoId { get; set; }
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
    }
} 