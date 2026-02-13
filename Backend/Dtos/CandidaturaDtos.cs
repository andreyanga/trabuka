using System;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class CandidaturaCreateDto
    {
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
    }

    public class CandidaturaReadDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public int ProjetoId { get; set; }
        public string DescricaoProjeto { get; set; }
        public DateTime DataCandidatura { get; set; }
        public StatusCandidatura Status { get; set; }
    }
}
