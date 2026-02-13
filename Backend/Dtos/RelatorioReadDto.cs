using System;

namespace TrabukaApi.Dtos
{
    public class RelatorioReadDto
    {
        public int Id { get; set; }
        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataEnvio { get; set; }
        public string Evidencias { get; set; }
        public string Descricao { get; set; }
        public string Feedback { get; set; }
        public int Status { get; set; }
    }
} 