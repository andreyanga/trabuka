using System;

namespace TrabukaApi.Dtos
{
    public class EquipeReadDto
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
    }
} 