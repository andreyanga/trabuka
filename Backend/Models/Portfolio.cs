using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrabukaApi.Models
{
    public class Portfolio
    {
        [Key]
        public int PortfolioId { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string URL { get; set; }
        public DateTime DataConclusao { get; set; }
        public string Imagem1 { get; set; }
        public string Imagem2 { get; set; }

        // Navegação
        public ICollection<Habilidade> Habilidades { get; set; }
        public ICollection<Experiencia> Experiencias { get; set; }
        public ICollection<Educacao> Educacoes { get; set; }
        public ICollection<Certificacao> Certificacoes { get; set; }
        public ICollection<ProjetoPortfolio> ProjetosPortfolio { get; set; }
        public ICollection<ImagemProjeto> ImagensProjetos { get; set; }
        public ICollection<Servico> Servicos { get; set; }
        public ICollection<Funcionalidade> Funcionalidades { get; set; }
        public ICollection<RedeSocial> RedesSociais { get; set; }
    }
} 