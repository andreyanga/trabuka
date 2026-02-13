using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;
using TrabukaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TrabukaApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly TrabukaDbContext _context;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(TrabukaDbContext context, ILogger<DashboardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DashboardResumoDto> GetDashboardResumoAsync(int usuarioId)
        {
            try
            {
                var dashboard = new DashboardResumoDto();

                // Buscar dados do usuário
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.UsuarioId == usuarioId);

                if (usuario == null)
                    throw new InvalidOperationException("Usuário não encontrado");

                // Preencher dados básicos do usuário
                dashboard.UsuarioId = usuario.UsuarioId;
                dashboard.Nome = usuario.Nome;
                dashboard.Email = usuario.Email;
                // Corrigir caminho da foto de perfil
                var fotoPerfil = usuario.FotoPerfil;
                if (!string.IsNullOrEmpty(fotoPerfil))
                {
                    // Se o caminho ainda contém "src/", remover e usar apenas o nome do arquivo
                    if (fotoPerfil.Contains("src/"))
                    {
                        fotoPerfil = Path.GetFileName(fotoPerfil);
                    }
                    dashboard.FotoPerfil = $"/assets/images/usuarios/{fotoPerfil}";
                }
                else
                {
                    dashboard.FotoPerfil = "/assets/images/default/default-user.png";
                }
                dashboard.NivelAtual = usuario.Nivel ?? NivelUsuario.Explorador;
                dashboard.CV = usuario.CV;
                dashboard.Habilidades = usuario.Habilidades;

                // Calcular progresso do nível
                var (progresso, proximoNivel) = CalcularProgressoNivel(usuario.Nivel ?? NivelUsuario.Explorador);
                dashboard.ProgressoNivel = progresso;
                dashboard.ProximoNivel = proximoNivel;
                dashboard.NivelNome = ObterNomeNivel(usuario.Nivel ?? NivelUsuario.Explorador);

                // Converter habilidades em lista
                if (!string.IsNullOrEmpty(usuario.Habilidades))
                {
                    dashboard.HabilidadesLista = usuario.Habilidades.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(h => h.Trim())
                        .ToList();
                }

                // Buscar certificações
                var portfolio = await _context.Portfolios
                    .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId);

                if (portfolio != null)
                {
                    var certificacoes = await _context.Certificacoes
                        .Where(c => c.PortfolioId == portfolio.PortfolioId)
                        .ToListAsync();

                    dashboard.Certificacoes = certificacoes.Select(c => new CertificacaoDto
                    {
                        Nome = c.Nome,
                        Ano = c.Ano
                    }).ToList();
                }

                // Calcular estatísticas de projetos
                var projetos = await _context.Projetos
                    .Where(p => p.Empresa != null)
                    .ToListAsync();

                dashboard.TotalProjetos = projetos.Count;
                dashboard.ProjetosConcluidos = projetos.Count(p => p.Status == StatusProjeto.Concluido);
                dashboard.ProjetosEmAndamento = projetos.Count(p => p.Status == StatusProjeto.Ativo);

                // Buscar projetos recentes
                var projetosRecentes = projetos
                    .OrderByDescending(p => p.Horario)
                    .Take(5)
                    .ToList();

                dashboard.ProjetosRecentes = projetosRecentes.Select(p => {
                    // Corrigir caminho da imagem de capa
                    var imagemCapa = p.ImagemCapa;
                    if (!string.IsNullOrEmpty(imagemCapa))
                    {
                        // Se o caminho ainda contém "src/", remover e usar apenas o nome do arquivo
                        if (imagemCapa.Contains("src/"))
                        {
                            imagemCapa = Path.GetFileName(imagemCapa);
                        }
                        
                        // Adicionar o caminho completo para acesso direto
                        imagemCapa = $"/assets/images/projetos/{imagemCapa}";
                    }

                    return new ProjetoResumoDto
                    {
                        Id = p.ProjetoId,
                        Descricao = p.Descricao,
                        Tipo = p.Tipo,
                        Status = p.Status,
                        DataInicio = p.Horario,
                        DataConclusao = p.Prazo,
                        Valor = p.Oramento,
                        ImagemCapa = imagemCapa
                    };
                }).ToList();

                // Calcular vagas aplicadas (baseado em tickets de suporte/duvida)
                var ticketsCandidatura = await _context.Tickets
                    .Where(t => t.UsuarioId == usuarioId && (t.Status == StatusTicket.Aberto || t.Status == StatusTicket.Pendente))
                    .ToListAsync();

                dashboard.VagasAplicadas = ticketsCandidatura.Count();

                // Calcular ganhos do mês atual e anterior
                var dataAtual = DateTime.Now;
                var inicioMesAtual = new DateTime(dataAtual.Year, dataAtual.Month, 1);
                var inicioMesAnterior = inicioMesAtual.AddMonths(-1);
                var fimMesAnterior = inicioMesAtual.AddDays(-1);

                var pagamentosMesAtual = await _context.Pagamentos
                    .Where(p => p.UsuarioId == usuarioId && 
                               p.Data >= inicioMesAtual && 
                               p.Status == StatusPagamento.Concluido)
                    .ToListAsync();

                var pagamentosMesAnterior = await _context.Pagamentos
                    .Where(p => p.UsuarioId == usuarioId && 
                               p.Data >= inicioMesAnterior && 
                               p.Data <= fimMesAnterior && 
                               p.Status == StatusPagamento.Concluido)
                    .ToListAsync();

                dashboard.GanhosMesAtual = pagamentosMesAtual.Sum(p => p.Valor);
                dashboard.GanhosMesAnterior = pagamentosMesAnterior.Sum(p => p.Valor);

                // Buscar pagamentos recentes
                var pagamentosRecentes = await _context.Pagamentos
                    .Include(p => p.Empresa)
                    .Where(p => p.UsuarioId == usuarioId)
                    .OrderByDescending(p => p.Data)
                    .Take(5)
                    .ToListAsync();

                dashboard.PagamentosRecentes = pagamentosRecentes.Select(p => new PagamentoResumoDto
                {
                    Id = p.PagamentoId,
                    Valor = p.Valor,
                    Data = p.Data,
                    Status = p.Status,
                    Empresa = p.Empresa?.Nome ?? "N/A"
                }).ToList();

                // Calcular testes realizados
                var testesRealizados = await _context.ResultadosTeste
                    .Where(rt => rt.id_usuario == usuarioId)
                    .CountAsync();

                dashboard.TestesRealizados = testesRealizados;

                // Calcular notificações não lidas
                var notificacoesNaoLidas = await _context.Notificacoes
                    .Where(n => n.UsuarioId == usuarioId && !n.Lida)
                    .CountAsync();

                dashboard.NotificacoesNaoLidas = notificacoesNaoLidas;

                // Gerar vagas recomendadas baseadas nas habilidades do usuário e no nível atual
                dashboard.VagasRecomendadas = await GerarVagasRecomendadasAsync(
                    usuario.Habilidades,
                    usuario.Nivel ?? NivelUsuario.Explorador);

                return dashboard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar dados do dashboard para usuário {UsuarioId}", usuarioId);
                throw;
            }
        }

        private (int progresso, string proximoNivel) CalcularProgressoNivel(NivelUsuario nivelAtual)
        {
            return nivelAtual switch
            {
                NivelUsuario.Explorador => (60, "Praticante"),
                NivelUsuario.Praticante => (75, "Construtor"),
                NivelUsuario.Construtor => (85, "Mestre"),
                NivelUsuario.Mestre => (100, "Máximo"),
                _ => (0, "Praticante")
            };
        }

        private string ObterNomeNivel(NivelUsuario nivel)
        {
            return nivel switch
            {
                NivelUsuario.Explorador => "Explorador",
                NivelUsuario.Praticante => "Praticante",
                NivelUsuario.Construtor => "Construtor",
                NivelUsuario.Mestre => "Mestre",
                _ => "Explorador"
            };
        }

        private async Task<List<VagaRecomendadaDto>> GerarVagasRecomendadasAsync(string habilidades, NivelUsuario nivelUsuario)
        {
            var vagasRecomendadas = new List<VagaRecomendadaDto>();

            if (string.IsNullOrEmpty(habilidades))
                return vagasRecomendadas;

            var habilidadesLista = habilidades.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(h => h.Trim().ToLower())
                .ToList();

            // Buscar projetos ativos
            var projetosAtivos = await _context.Projetos
                .Include(p => p.Empresa)
                .Where(p => p.Status == StatusProjeto.Ativo)
                .ToListAsync();

            // Regra de negócio: estudante só vê vagas do seu nível ou inferior.
            // Como ainda não existe um campo dedicado de nível na entidade Projeto,
            // usamos o campo Tipo para codificar o nível mínimo da vaga:
            // "Inicial", "Intermediario", "Avancado", "Master".
            int nivelUsuarioRank = ObterRankNivel(nivelUsuario);

            var projetosRecomendados = projetosAtivos
                .Where(p => ObterRankNivelProjeto(p.Tipo) <= nivelUsuarioRank)
                .OrderByDescending(p => p.Horario)
                .Take(3)
                .ToList();

            foreach (var projeto in projetosRecomendados)
            {
                // Corrigir caminho da imagem de capa
                var imagemCapa = projeto.ImagemCapa;
                if (!string.IsNullOrEmpty(imagemCapa))
                {
                    // Se o caminho ainda contém "src/", remover e usar apenas o nome do arquivo
                    if (imagemCapa.Contains("src/"))
                    {
                        imagemCapa = Path.GetFileName(imagemCapa);
                    }
                    
                    // Adicionar o caminho completo para acesso direto
                    imagemCapa = $"/assets/images/projetos/{imagemCapa}";
                }

                var vaga = new VagaRecomendadaDto
                {
                    Id = projeto.ProjetoId,
                    Titulo = projeto.Descricao,
                    Empresa = projeto.Empresa?.Nome ?? "Empresa não informada",
                    Localizacao = "Luanda", // Pode ser expandido para incluir localização real
                    Descricao = $"Projeto {projeto.Tipo} com orçamento de Kz {projeto.Oramento:N0}",
                    HabilidadesRequeridas = habilidadesLista.Take(3).ToList(),
                    DataPublicacao = projeto.Horario,
                    TempoPublicacao = CalcularTempoPublicacao(projeto.Horario),
                    ImagemCapa = imagemCapa
                };

                vagasRecomendadas.Add(vaga);
            }

            return vagasRecomendadas;
        }

        private string CalcularTempoPublicacao(DateTime dataPublicacao)
        {
            var diferenca = DateTime.Now - dataPublicacao;

            if (diferenca.TotalDays >= 1)
                return $"há {(int)diferenca.TotalDays} dias";
            else if (diferenca.TotalHours >= 1)
                return $"há {(int)diferenca.TotalHours} horas";
            else
                return "há poucos minutos";
        }

        /// <summary>
        /// Converte o nível do usuário em um rank numérico para comparação.
        /// </summary>
        private int ObterRankNivel(NivelUsuario nivel)
        {
            return nivel switch
            {
                NivelUsuario.Explorador => 1,   // equivalente a vagas de nível "Inicial"
                NivelUsuario.Praticante => 2,   // pode ver "Inicial" e "Intermediario"
                NivelUsuario.Construtor => 3,   // pode ver até "Avancado"
                NivelUsuario.Mestre => 4,       // pode ver todos, incluindo "Master"
                _ => 1
            };
        }

        /// <summary>
        /// Converte o campo Tipo do projeto em um rank de nível.
        /// Espera valores como: "Inicial", "Intermediario", "Avancado", "Master".
        /// </summary>
        private int ObterRankNivelProjeto(string tipoProjeto)
        {
            if (string.IsNullOrWhiteSpace(tipoProjeto))
                return 1;

            var valor = tipoProjeto.Trim().ToLower();

            return valor switch
            {
                "inicial" => 1,
                "intermediario" => 2,
                "avancado" => 3,
                "master" => 4,
                _ => 1
            };
        }
    }
} 