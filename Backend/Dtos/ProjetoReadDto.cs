using System;

namespace TrabukaApi.Dtos
{
    public class ProjetoReadDto
    {
        public int ProjetoId { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Oramento { get; set; }
        public DateTime Horario { get; set; }
        public DateTime Prazo { get; set; }
        public int EmpresaId { get; set; }
        public int? MentorId { get; set; }
        public int Status { get; set; }
        public string ImagemCapa { get; set; }
        public int Id { get; set; }
    }
} 