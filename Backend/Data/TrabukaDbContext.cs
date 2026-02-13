using Microsoft.EntityFrameworkCore;
using TrabukaApi.Models;

namespace TrabukaApi.Data
{
    public class TrabukaDbContext : DbContext
    {
        public TrabukaDbContext(DbContextOptions<TrabukaDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Teste> Testes { get; set; }
        public DbSet<Questao> Questoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<ResultadoTeste> ResultadosTeste { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<Experiencia> Experiencias { get; set; }
        public DbSet<Educacao> Educacoes { get; set; }
        public DbSet<Certificacao> Certificacoes { get; set; }
        public DbSet<ProjetoPortfolio> ProjetosPortfolio { get; set; }
        public DbSet<ImagemProjeto> ImagensProjetos { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Funcionalidade> Funcionalidades { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }
        public DbSet<ServicosRelacionados> ServicosRelacionados { get; set; }
        public DbSet<Metrica> Metricas { get; set; }
        public DbSet<Mentoria> Mentorias { get; set; }
        public DbSet<Sessao> Sessoes { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<UsuarioEquipe> UsuarioEquipes { get; set; }
        public DbSet<Candidatura> Candidaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chave composta para UsuarioEquipe
            modelBuilder.Entity<UsuarioEquipe>()
                .HasKey(ue => new { ue.usuario_id, ue.equipe_id });

            // Chave composta para ServicosRelacionados
            modelBuilder.Entity<ServicosRelacionados>()
                .HasKey(sr => new { sr.servico_id, sr.relacionado_id });

            // Relacionamento N:N entre Servico e Servico (ServicosRelacionados)
            modelBuilder.Entity<ServicosRelacionados>()
                .HasOne(sr => sr.Servico)
                .WithMany()
                .HasForeignKey(sr => sr.servico_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ServicosRelacionados>()
                .HasOne(sr => sr.ServicoRelacionado)
                .WithMany()
                .HasForeignKey(sr => sr.relacionado_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento N:N entre Usuario e Equipe (UsuarioEquipe)
            modelBuilder.Entity<UsuarioEquipe>()
                .HasOne(ue => ue.Usuario)
                .WithMany(u => u.UsuarioEquipes)
                .HasForeignKey(ue => ue.usuario_id);

            modelBuilder.Entity<UsuarioEquipe>()
                .HasOne(ue => ue.Equipe)
                .WithMany(e => e.UsuarioEquipes)
                .HasForeignKey(ue => ue.equipe_id);

            // Restringir deleção em cascata para as FKs de Mentoria
            modelBuilder.Entity<Mentoria>()
                .HasOne(m => m.Mentor)
                .WithMany()
                .HasForeignKey(m => m.mentor_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mentoria>()
                .HasOne(m => m.Mentorado)
                .WithMany()
                .HasForeignKey(m => m.mentorado_id)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurar precisão decimal para Metrica.valor
            modelBuilder.Entity<Metrica>()
                .Property(m => m.valor)
                .HasPrecision(18, 2);

            // Configurar precisão decimal para Pagamento.Valor
            modelBuilder.Entity<Pagamento>()
                .Property(p => p.Valor)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }

        // Removido o método Seed()


      }
} 