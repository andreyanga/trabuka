using TrabukaApi.Models.Enums;

namespace TrabukaApi.Dtos
{
    public class DashboardResumoDto
    {
        // Dados do usuário
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string FotoPerfil { get; set; }
        public NivelUsuario NivelAtual { get; set; }
        public string NivelNome { get; set; }
        public int ProgressoNivel { get; set; }
        public string ProximoNivel { get; set; }

        // Estatísticas do dashboard
        public int TotalProjetos { get; set; }
        public int ProjetosConcluidos { get; set; }
        public int ProjetosEmAndamento { get; set; }
        public int VagasAplicadas { get; set; }
        public decimal GanhosMesAtual { get; set; }
        public decimal GanhosMesAnterior { get; set; }
        public int TestesRealizados { get; set; }
        public int NotificacoesNaoLidas { get; set; }

        // Dados do perfil
        public string CV { get; set; }
        public string Habilidades { get; set; }
        public List<string> HabilidadesLista { get; set; } = new List<string>();
        public List<CertificacaoDto> Certificacoes { get; set; } = new List<CertificacaoDto>();

        // Vagas recomendadas
        public List<VagaRecomendadaDto> VagasRecomendadas { get; set; } = new List<VagaRecomendadaDto>();

        // Projetos recentes
        public List<ProjetoResumoDto> ProjetosRecentes { get; set; } = new List<ProjetoResumoDto>();

        // Pagamentos recentes
        public List<PagamentoResumoDto> PagamentosRecentes { get; set; } = new List<PagamentoResumoDto>();
    }

    public class CertificacaoDto
    {
        public string Nome { get; set; }
        public int Ano { get; set; }
    }

    public class VagaRecomendadaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Localizacao { get; set; }
        public string Descricao { get; set; }
        public List<string> HabilidadesRequeridas { get; set; } = new List<string>();
        public DateTime DataPublicacao { get; set; }
        public string TempoPublicacao { get; set; }
        public string ImagemCapa { get; set; }
    }

    public class ProjetoResumoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public StatusProjeto Status { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal? Valor { get; set; }
        public string ImagemCapa { get; set; }
    }

    public class PagamentoResumoDto
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public StatusPagamento Status { get; set; }
        public string Empresa { get; set; }
    }
} 