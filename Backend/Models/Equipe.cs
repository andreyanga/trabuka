using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Equipe
    {
        [Key]
        public int id { get; set; }

        public int projeto_id { get; set; }
        public Projeto Projeto { get; set; } = null!;

        public string nome { get; set; } = null!;
        public DateTime data_criacao { get; set; }
        public string status { get; set; } = null!; // "ativo, concluido"
        public DateTime created_at { get; set; }

        // Navegação
        public ICollection<UsuarioEquipe> UsuarioEquipes { get; set; } = null!;
    }
} 