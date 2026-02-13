using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Dtos
{
    public class ProjetoCreateUploadDto
    {
        [Required]
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int Oramento { get; set; }
        public DateTime Horario { get; set; }
        public DateTime Prazo { get; set; }
        [Required]
        public int EmpresaId { get; set; }
        public int? MentorId { get; set; }
        public int Status { get; set; }
        public IFormFile ImagemCapa { get; set; }
    }
} 