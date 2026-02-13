using TrabukaApi.Models;
using TrabukaApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabukaApi.Data
{
    public static class DatabaseSeeder
    {
        public static void Seed(TrabukaDbContext context)
        {
            // =========================
            // USUÁRIOS E EMPRESAS
            // =========================
            string HashPassword(string senha)
            {
                using var sha256 = System.Security.Cryptography.SHA256.Create();
                var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(hashedBytes);
            }

            // Gestor + 8 estudantes + 1 usuário de suporte (extra)
            // Verificar e criar apenas os usuários que não existem (por email)
            var emailsSeed = new[] {
                "gestor@trabuka.com",
                "jovem1@trabuka.com", "jovem2@trabuka.com",
                "jovem3@trabuka.com", "jovem4@trabuka.com",
                "jovem5@trabuka.com", "jovem6@trabuka.com",
                "jovem7@trabuka.com", "jovem8@trabuka.com",
                "suporte@trabuka.com"
            };

            var usuariosExistentes = context.Usuarios
                .Where(u => emailsSeed.Contains(u.Email))
                .Select(u => u.Email)
                .ToList();

            var usuariosParaCriar = new List<Usuario>();

            if (!usuariosExistentes.Contains("gestor@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Gestor Trabuka",
                    Email = "gestor@trabuka.com",
                    SenhaHash = HashPassword("Gestor@123"),
                    TipoUsuario = TipoUsuario.Gestor,
                    Telefone = "900000001",
                    CV = "Gestor responsável pela aprovação de usuários, vagas e relatórios.",
                    Habilidades = "Gestão de Projetos, Avaliação Técnica",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Mestre,
                    Status = StatusUsuario.Ativo // Gestor já começa ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem1@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "João Explorador",
                    Email = "jovem1@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000010",
                    CV = "Estudante de TI focado em desenvolvimento web.",
                    Habilidades = "HTML, CSS, JavaScript",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Explorador,
                    Status = StatusUsuario.Ativo // Usuários do seed começam ativos para login
                });
            }

            if (!usuariosExistentes.Contains("jovem2@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Maria Exploradora",
                    Email = "jovem2@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000011",
                    CV = "Estudante interessada em UX/UI.",
                    Habilidades = "HTML, CSS, Figma",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Explorador,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem3@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Carlos Praticante",
                    Email = "jovem3@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000012",
                    CV = "Júnior em desenvolvimento backend.",
                    Habilidades = "C#, .NET, SQL",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Praticante,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem4@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Ana Praticante",
                    Email = "jovem4@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000013",
                    CV = "Estudante com foco em front-end.",
                    Habilidades = "Angular, TypeScript",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Praticante,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem5@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Bruno Construtor",
                    Email = "jovem5@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000014",
                    CV = "Dev fullstack em formação.",
                    Habilidades = "Angular, .NET, SQL",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Construtor,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem6@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Carla Construtora",
                    Email = "jovem6@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000015",
                    CV = "Experiência com APIs REST.",
                    Habilidades = "Node.js, MongoDB",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Construtor,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem7@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Diego Mestre",
                    Email = "jovem7@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000016",
                    CV = "Experiência sólida em projetos reais.",
                    Habilidades = "Arquitetura, DevOps",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Mestre,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("jovem8@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Eva Mestra",
                    Email = "jovem8@trabuka.com",
                    SenhaHash = HashPassword("Jovem@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000017",
                    CV = "Forte atuação em liderança técnica.",
                    Habilidades = "Liderança, Code Review",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Mestre,
                    Status = StatusUsuario.Ativo
                });
            }

            if (!usuariosExistentes.Contains("suporte@trabuka.com"))
            {
                usuariosParaCriar.Add(new Usuario {
                    Nome = "Suporte Trabuka",
                    Email = "suporte@trabuka.com",
                    SenhaHash = HashPassword("Suporte@123"),
                    TipoUsuario = TipoUsuario.Suporte,
                    Telefone = "900000020",
                    CV = "Responsável por atendimento aos usuários.",
                    Habilidades = "Atendimento, Suporte Técnico",
                    FotoPerfil = "",
                    Nivel = NivelUsuario.Explorador,
                    Status = StatusUsuario.Ativo
                });
            }

            // Adicionar usuários pendentes à lista antes de salvar
            if (usuariosParaCriar.Any())
            {
                context.Usuarios.AddRange(usuariosParaCriar);
                context.SaveChanges();
            }

            // EMPRESAS (3 empresas em setores distintos)
            var emailsEmpresas = new[] { "empresa1@trabuka.com", "empresa2@trabuka.com", "empresa3@trabuka.com" };
            var empresasExistentes = context.Empresas
                .Where(e => emailsEmpresas.Contains(e.Email))
                .Select(e => e.Email)
                .ToList();

            var empresasParaCriar = new List<Empresa>();

            if (!empresasExistentes.Contains("empresa1@trabuka.com"))
            {
                empresasParaCriar.Add(new Empresa { Nome = "Tech Angola", Email = "empresa1@trabuka.com", Setor = "Tecnologia", Contato = "22223333" });
            }

            if (!empresasExistentes.Contains("empresa2@trabuka.com"))
            {
                empresasParaCriar.Add(new Empresa { Nome = "EducaMais", Email = "empresa2@trabuka.com", Setor = "Educação", Contato = "33334444" });
            }

            if (!empresasExistentes.Contains("empresa3@trabuka.com"))
            {
                empresasParaCriar.Add(new Empresa { Nome = "Saude+ Angola", Email = "empresa3@trabuka.com", Setor = "Saúde", Contato = "44445555" });
            }

            if (empresasParaCriar.Any())
            {
                context.Empresas.AddRange(empresasParaCriar);
                context.SaveChanges();
            }

            // Criar alguns usuários pendentes para teste do gestor (após salvar os outros)
            var emailsPendentes = new[] { "pendente1@trabuka.com", "pendente2@trabuka.com" };
            var usuariosPendentesExistentes = context.Usuarios
                .Where(u => emailsPendentes.Contains(u.Email))
                .Select(u => u.Email)
                .ToList();

            var usuariosPendentesParaCriar = new List<Usuario>();

            if (!usuariosPendentesExistentes.Contains("pendente1@trabuka.com"))
            {
                usuariosPendentesParaCriar.Add(new Usuario {
                    Nome = "Estudante Pendente 1",
                    Email = "pendente1@trabuka.com",
                    SenhaHash = HashPassword("Pendente@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000100",
                    CV = "Aguardando aprovação do gestor.",
                    Habilidades = "HTML, CSS",
                    FotoPerfil = "",
                    Nivel = null,
                    Status = StatusUsuario.Pendente
                });
            }

            if (!usuariosPendentesExistentes.Contains("pendente2@trabuka.com"))
            {
                usuariosPendentesParaCriar.Add(new Usuario {
                    Nome = "Estudante Pendente 2",
                    Email = "pendente2@trabuka.com",
                    SenhaHash = HashPassword("Pendente@123"),
                    TipoUsuario = TipoUsuario.Jovem,
                    Telefone = "900000101",
                    CV = "Aguardando aprovação do gestor.",
                    Habilidades = "JavaScript",
                    FotoPerfil = "",
                    Nivel = null,
                    Status = StatusUsuario.Pendente
                });
            }

            if (usuariosPendentesParaCriar.Any())
            {
                context.Usuarios.AddRange(usuariosPendentesParaCriar);
                context.SaveChanges();
            }

            // =========================
            // VAGAS / PROJETOS
            // =========================
            var empresas = context.Empresas.Take(3).ToList();
            if (empresas.Count >= 3)
            {
                var empresa1 = empresas[0];
                var empresa2 = empresas[1];
                var empresa3 = empresas[2];

                var projetosExistentes = context.Projetos.Any();
                var projetosParaCriar = new List<Projeto>();

                if (!projetosExistentes)
                {
                    // 6 vagas publicadas e aprovadas (Status Ativo) em níveis diferentes
                    projetosParaCriar.AddRange(new List<Projeto>
                    {
                        new Projeto {
                            Descricao = "Landing page institucional",
                            Tipo = "Inicial",
                            Oramento = 50000,
                            Horario = DateTime.Now.AddDays(-15),
                            Prazo = DateTime.Now.AddMonths(1),
                            EmpresaId = empresa1.EmpresaId,
                            Status = StatusProjeto.Ativo,
                            ImagemCapa = ""
                        },
                    new Projeto {
                        Descricao = "Dashboard interno simples",
                        Tipo = "Inicial",
                        Oramento = 80000,
                        Horario = DateTime.Now.AddDays(-10),
                        Prazo = DateTime.Now.AddMonths(2),
                        EmpresaId = empresa2.EmpresaId,
                        Status = StatusProjeto.Ativo,
                        ImagemCapa = ""
                    },
                    new Projeto {
                        Descricao = "Portal de cursos online",
                        Tipo = "Intermediario",
                        Oramento = 150000,
                        Horario = DateTime.Now.AddDays(-20),
                        Prazo = DateTime.Now.AddMonths(3),
                        EmpresaId = empresa1.EmpresaId,
                        Status = StatusProjeto.Ativo,
                        ImagemCapa = ""
                    },
                    new Projeto {
                        Descricao = "App mobile de agendamento médico",
                        Tipo = "Intermediario",
                        Oramento = 180000,
                        Horario = DateTime.Now.AddDays(-5),
                        Prazo = DateTime.Now.AddMonths(4),
                        EmpresaId = empresa3.EmpresaId,
                        Status = StatusProjeto.Ativo,
                        ImagemCapa = ""
                    },
                    new Projeto {
                        Descricao = "Plataforma de pagamentos",
                        Tipo = "Avancado",
                        Oramento = 250000,
                        Horario = DateTime.Now.AddDays(-30),
                        Prazo = DateTime.Now.AddMonths(5),
                        EmpresaId = empresa2.EmpresaId,
                        Status = StatusProjeto.Ativo,
                        ImagemCapa = ""
                    },
                    new Projeto {
                        Descricao = "Arquitetura distribuída para marketplace",
                        Tipo = "Master",
                        Oramento = 400000,
                        Horario = DateTime.Now.AddDays(-40),
                        Prazo = DateTime.Now.AddMonths(6),
                        EmpresaId = empresa1.EmpresaId,
                        Status = StatusProjeto.Ativo,
                        ImagemCapa = ""
                    }
                });
                context.SaveChanges();
            }

            // Criar alguns projetos pendentes para teste do gestor
            var projetosPendentesExistentes = context.Projetos
                .Where(p => p.Status == StatusProjeto.Pendente && p.Descricao.Contains("PENDENTE"))
                .Any();

            if (!projetosPendentesExistentes && empresas.Count > 0)
            {
                var empresaPendente = empresas[0];
                context.Projetos.AddRange(new List<Projeto>
                {
                    new Projeto {
                        Descricao = "Sistema de gestão escolar - PENDENTE",
                        Tipo = "Intermediario",
                        Oramento = 120000,
                        Horario = DateTime.Now,
                        Prazo = DateTime.Now.AddMonths(3),
                        EmpresaId = empresaPendente.EmpresaId,
                        Status = StatusProjeto.Pendente,
                        ImagemCapa = ""
                    },
                    new Projeto {
                        Descricao = "App mobile de delivery - PENDENTE",
                        Tipo = "Avancado",
                        Oramento = 200000,
                        Horario = DateTime.Now,
                        Prazo = DateTime.Now.AddMonths(4),
                        EmpresaId = empresaPendente.EmpresaId,
                        Status = StatusProjeto.Pendente,
                        ImagemCapa = ""
                    }
                });
                context.SaveChanges();
            }

            // PORTFOLIOS
            if (!context.Portfolios.Any())
            {
                var usuario = context.Usuarios.First();
                context.Portfolios.Add(new Portfolio { UsuarioId = usuario.UsuarioId, URL = "http://portfolio.joao.com", DataConclusao = DateTime.Now, Imagem1 = "", Imagem2 = "" });
                context.SaveChanges();
            }

            // PAGAMENTOS
            if (!context.Pagamentos.Any())
            {
                var usuario = context.Usuarios.First();
                var empresa = context.Empresas.First();
                context.Pagamentos.Add(new Pagamento { UsuarioId = usuario.UsuarioId, EmpresaId = empresa.EmpresaId, Valor = 1000, Data = DateTime.Now, Status = StatusPagamento.Concluido });
                context.SaveChanges();
            }

            // TICKETS
            if (!context.Tickets.Any())
            {
                var usuario = context.Usuarios.First();
                context.Tickets.Add(new Ticket { UsuarioId = usuario.UsuarioId, Descricao = "Dúvida sobre cadastro", Status = StatusTicket.Aberto, DataCriacao = DateTime.Now });
                context.SaveChanges();
            }

            // FAQ
            if (!context.FAQs.Any())
            {
                context.FAQs.Add(new FAQ { Pergunta = "Como cadastrar?", Resposta = "Clique em registrar e preencha o formulário.", DataPublicacao = DateTime.Now });
                context.SaveChanges();
            }

            // NOTIFICAÇÕES
            if (!context.Notificacoes.Any())
            {
                var usuario = context.Usuarios.First();
                context.Notificacoes.Add(new Notificacao { UsuarioId = usuario.UsuarioId, Mensagem = "Bem-vindo ao sistema!", Lida = false, DataEnvio = DateTime.Now });
                context.SaveChanges();
            }

            // RELATÓRIOS (inclui exemplos com avaliação do gestor)
            if (!context.Relatorios.Any())
            {
                var gestor = context.Usuarios.First(u => u.TipoUsuario == TipoUsuario.Gestor);
                var estudantes = context.Usuarios.Where(u => u.TipoUsuario == TipoUsuario.Jovem).ToList();
                var projetos = context.Projetos.ToList();

                // Relatório 1 - estudante aprovado com nota alta (pode ser promovido)
                context.Relatorios.Add(new Relatorio {
                    Projeto = projetos[2], // Projeto de nível Intermediario
                    Usuario = estudantes[4], // Bruno Construtor
                    data_envio = DateTime.Now.AddDays(-3),
                    evidencias = "https://github.com/bruno/trabuka-projeto-portal-cursos",
                    descricao = "Implementação completa do portal de cursos, com testes básicos.",
                    feedback = "Ótimo trabalho. Nota: 9",
                    status = StatusRelatorio.Aprovado
                });

                // Relatório 2 - estudante aprovado em projeto avançado
                context.Relatorios.Add(new Relatorio {
                    Projeto = projetos[4], // Projeto de nível Avancado
                    Usuario = estudantes[5], // Carla Construtora
                    data_envio = DateTime.Now.AddDays(-1),
                    evidencias = "https://github.com/carla/trabuka-plataforma-pagamentos",
                    descricao = "Entrega da API de pagamentos com documentação básica.",
                    feedback = "Entrega sólida. Nota: 8",
                    status = StatusRelatorio.Aprovado
                });

                // Exemplo pendente para outros estudantes
                context.Relatorios.Add(new Relatorio {
                    Projeto = projetos[0],
                    Usuario = estudantes[0],
                    data_envio = DateTime.Now,
                    evidencias = "https://github.com/joao/trabuka-landing-page",
                    descricao = "Primeira versão da landing page.",
                    feedback = "",
                    status = StatusRelatorio.Pendente
                });

                context.SaveChanges();
            }

            // TESTES E QUESTÕES
            if (!context.Testes.Any())
            {
                var testeInicial = new Teste 
                { 
                    Nome = "Teste Inicial - HTML/CSS/JavaScript Básico", 
                    Descricao = "Teste para avaliar conhecimentos básicos de desenvolvimento web",
                    Dificuldade = DificuldadeTeste.Inicial, 
                    PontuacaoMaxima = 100,
                    PontuacaoMinima = 70, // 70/100 para aprovação
                    TempoLimiteMinutos = 60,
                    Ativo = true,
                    DataCriacao = DateTime.Now
                };
                context.Testes.Add(testeInicial);
                context.SaveChanges();

                // Adicionar questões do teste
                var questoes = new List<Questao>
                {
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que significa HTML?",
                        OpcaoA = "HyperText Markup Language",
                        OpcaoB = "High Tech Modern Language",
                        OpcaoC = "Home Tool Markup Language",
                        OpcaoD = "Hyperlink Text Markup Language",
                        RespostaCorreta = "A",
                        Pontuacao = 5,
                        Ordem = 1,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual tag HTML é usada para criar um link?",
                        OpcaoA = "<link>",
                        OpcaoB = "<a>",
                        OpcaoC = "<url>",
                        OpcaoD = "<href>",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 2,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Em CSS, qual propriedade é usada para mudar a cor do texto?",
                        OpcaoA = "text-color",
                        OpcaoB = "font-color",
                        OpcaoC = "color",
                        OpcaoD = "text-style",
                        RespostaCorreta = "C",
                        Pontuacao = 5,
                        Ordem = 3,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual método JavaScript é usado para selecionar um elemento pelo ID?",
                        OpcaoA = "getElementById()",
                        OpcaoB = "querySelector()",
                        OpcaoC = "getElement()",
                        OpcaoD = "selectById()",
                        RespostaCorreta = "A",
                        Pontuacao = 5,
                        Ordem = 4,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que é uma variável em JavaScript?",
                        OpcaoA = "Um valor fixo que não pode mudar",
                        OpcaoB = "Um container para armazenar dados",
                        OpcaoC = "Uma função",
                        OpcaoD = "Um tipo de loop",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 5,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual tag HTML é usada para criar uma lista não ordenada?",
                        OpcaoA = "<ol>",
                        OpcaoB = "<ul>",
                        OpcaoC = "<li>",
                        OpcaoD = "<list>",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 6,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Em CSS, o que significa 'margin'?",
                        OpcaoA = "Espaço dentro do elemento",
                        OpcaoB = "Espaço fora do elemento",
                        OpcaoC = "Cor de fundo",
                        OpcaoD = "Tamanho da fonte",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 7,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual operador JavaScript é usado para comparação de igualdade estrita?",
                        OpcaoA = "==",
                        OpcaoB = "===",
                        OpcaoC = "=",
                        OpcaoD = "!=",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 8,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual tag HTML é usada para criar um parágrafo?",
                        OpcaoA = "<paragraph>",
                        OpcaoB = "<p>",
                        OpcaoC = "<para>",
                        OpcaoD = "<text>",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 9,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que é um array em JavaScript?",
                        OpcaoA = "Uma variável única",
                        OpcaoB = "Uma estrutura de dados que armazena múltiplos valores",
                        OpcaoC = "Uma função",
                        OpcaoD = "Um objeto",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 10,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual propriedade CSS é usada para mudar o tamanho da fonte?",
                        OpcaoA = "text-size",
                        OpcaoB = "font-size",
                        OpcaoC = "size",
                        OpcaoD = "font-style",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 11,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Como você declara uma função em JavaScript?",
                        OpcaoA = "function nomeFuncao()",
                        OpcaoB = "func nomeFuncao()",
                        OpcaoC = "def nomeFuncao()",
                        OpcaoD = "method nomeFuncao()",
                        RespostaCorreta = "A",
                        Pontuacao = 5,
                        Ordem = 12,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual atributo HTML é usado para adicionar uma imagem?",
                        OpcaoA = "src",
                        OpcaoB = "img",
                        OpcaoC = "image",
                        OpcaoD = "picture",
                        RespostaCorreta = "A",
                        Pontuacao = 5,
                        Ordem = 13,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que significa 'responsive design'?",
                        OpcaoA = "Design que responde rápido",
                        OpcaoB = "Design que se adapta a diferentes tamanhos de tela",
                        OpcaoC = "Design colorido",
                        OpcaoD = "Design animado",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 14,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual método JavaScript é usado para adicionar um elemento ao final de um array?",
                        OpcaoA = "add()",
                        OpcaoB = "push()",
                        OpcaoC = "append()",
                        OpcaoD = "insert()",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 15,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Em CSS, qual propriedade é usada para centralizar texto?",
                        OpcaoA = "align: center",
                        OpcaoB = "text-align: center",
                        OpcaoC = "center: true",
                        OpcaoD = "text-center",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 16,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que é um evento em JavaScript?",
                        OpcaoA = "Uma variável",
                        OpcaoB = "Uma ação que ocorre quando o usuário interage com a página",
                        OpcaoC = "Uma função",
                        OpcaoD = "Um objeto",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 17,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual tag HTML é usada para criar um formulário?",
                        OpcaoA = "<input>",
                        OpcaoB = "<form>",
                        OpcaoC = "<fieldset>",
                        OpcaoD = "<submit>",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 18,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "O que é CSS?",
                        OpcaoA = "Computer Style Sheets",
                        OpcaoB = "Cascading Style Sheets",
                        OpcaoC = "Creative Style System",
                        OpcaoD = "Color Style Sheets",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 19,
                        CreatedAt = DateTime.Now
                    },
                    new Questao 
                    { 
                        TesteId = testeInicial.TesteId,
                        Enunciado = "Qual método JavaScript é usado para converter uma string em número?",
                        OpcaoA = "toNumber()",
                        OpcaoB = "parseInt() ou Number()",
                        OpcaoC = "convert()",
                        OpcaoD = "stringToNumber()",
                        RespostaCorreta = "B",
                        Pontuacao = 5,
                        Ordem = 20,
                        CreatedAt = DateTime.Now
                    }
                };

                context.Questoes.AddRange(questoes);
                context.SaveChanges();
            }

            // MENTORIAS (usadas aqui como vínculo gestor-estudante-projeto)
            if (!context.Mentorias.Any())
            {
                var mentor = context.Usuarios.First(u => u.TipoUsuario == TipoUsuario.Gestor);
                var estudantes = context.Usuarios.Where(u => u.TipoUsuario == TipoUsuario.Jovem).ToList();
                var projetos = context.Projetos.ToList();

                // Mentorias/candidaturas aprovadas para pelo menos 4 estudantes
                context.Mentorias.AddRange(new List<Mentoria>
                {
                    new Mentoria {
                        Mentor = mentor,
                        Mentorado = estudantes[0],
                        Projeto = projetos[0],
                        data_inicio = DateTime.Now.AddDays(-7),
                        data_fim = DateTime.Now.AddMonths(1),
                        feedback = "Acompanhamento inicial",
                        created_at = DateTime.Now
                    },
                    new Mentoria {
                    Mentor = mentor,
                        Mentorado = estudantes[1],
                        Projeto = projetos[1],
                        data_inicio = DateTime.Now.AddDays(-10),
                    data_fim = DateTime.Now.AddMonths(1),
                        feedback = "Boa evolução",
                        created_at = DateTime.Now
                    },
                    new Mentoria {
                        Mentor = mentor,
                        Mentorado = estudantes[4],
                        Projeto = projetos[2],
                        data_inicio = DateTime.Now.AddDays(-15),
                        data_fim = DateTime.Now.AddMonths(2),
                        feedback = "Destaque técnico",
                        created_at = DateTime.Now
                    },
                    new Mentoria {
                        Mentor = mentor,
                        Mentorado = estudantes[5],
                        Projeto = projetos[4],
                        data_inicio = DateTime.Now.AddDays(-20),
                        data_fim = DateTime.Now.AddMonths(2),
                        feedback = "Bom desempenho geral",
                    created_at = DateTime.Now
                    }
                });
                context.SaveChanges();
            }

            // EQUIPES (ligação prática de estudantes nos projetos)
            if (!context.Equipes.Any())
            {
                var projetos = context.Projetos.ToList();
                context.Equipes.AddRange(new List<Equipe>
                {
                    new Equipe {
                        Projeto = projetos[0],
                        nome = "Equipe Landing Page",
                        data_criacao = DateTime.Now.AddDays(-7),
                        status = "ativo",
                        created_at = DateTime.Now
                    },
                    new Equipe {
                        Projeto = projetos[2],
                        nome = "Equipe Portal Cursos",
                        data_criacao = DateTime.Now.AddDays(-10),
                        status = "ativo",
                        created_at = DateTime.Now
                    },
                    new Equipe {
                        Projeto = projetos[4],
                        nome = "Equipe Pagamentos",
                        data_criacao = DateTime.Now.AddDays(-12),
                    status = "ativo",
                    created_at = DateTime.Now
                    }
                });
                context.SaveChanges();
            }

            // USUARIOEQUIPE (candidaturas aprovadas / estudantes alocados)
            if (!context.UsuarioEquipes.Any())
            {
                var estudantes = context.Usuarios.Where(u => u.TipoUsuario == TipoUsuario.Jovem).ToList();
                var equipes = context.Equipes.ToList();

                // Pelo menos 4 estudantes alocados em projetos (candidaturas aprovadas)
                context.UsuarioEquipes.AddRange(new List<UsuarioEquipe>
                {
                    new UsuarioEquipe { usuario_id = estudantes[0].UsuarioId, equipe_id = equipes[0].id, papel = PapelEquipe.Membro, data_entrada = DateTime.Now.AddDays(-5), created_at = DateTime.Now },
                    new UsuarioEquipe { usuario_id = estudantes[1].UsuarioId, equipe_id = equipes[0].id, papel = PapelEquipe.Membro, data_entrada = DateTime.Now.AddDays(-5), created_at = DateTime.Now },
                    new UsuarioEquipe { usuario_id = estudantes[4].UsuarioId, equipe_id = equipes[1].id, papel = PapelEquipe.Membro, data_entrada = DateTime.Now.AddDays(-8), created_at = DateTime.Now },
                    new UsuarioEquipe { usuario_id = estudantes[5].UsuarioId, equipe_id = equipes[2].id, papel = PapelEquipe.Membro, data_entrada = DateTime.Now.AddDays(-9), created_at = DateTime.Now }
                });
                context.SaveChanges();
            }

            // CONFIGURAÇÕES
            if (!context.Configuracoes.Any())
            {
                var usuario = context.Usuarios.First();
                context.Configuracoes.Add(new Configuracao {
                    Usuario = usuario,
                    chave = "tema",
                    valor = "claro",
                    descricao = "Tema do sistema",
                    created_at = DateTime.Now
                });
                context.SaveChanges();
            }

            // HABILIDADES
            if (!context.Habilidades.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Habilidades.Add(new Habilidade { PortfolioId = portfolio.PortfolioId, Categoria = "Backend", Nome = "C#", Percentual = 90, Descricao = "Desenvolvimento .NET", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // EXPERIÊNCIAS
            if (!context.Experiencias.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Experiencias.Add(new Experiencia { PortfolioId = portfolio.PortfolioId, Cargo = "Desenvolvedor", Empresa = "Tech Angola", periodo_inicio = DateTime.Now.AddYears(-1), periodo_fim = DateTime.Now, Conquistas = "Projeto X", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // EDUCAÇÃO
            if (!context.Educacoes.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Educacoes.Add(new Educacao { PortfolioId = portfolio.PortfolioId, Curso = "Engenharia de Software", Instituicao = "Universidade ABC", periodo_inicio = DateTime.Now.AddYears(-4), periodo_fim = DateTime.Now.AddYears(-1), Descricao = "Graduação completa", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // CERTIFICAÇÕES
            if (!context.Certificacoes.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Certificacoes.Add(new Certificacao { PortfolioId = portfolio.PortfolioId, Nome = ".NET", Ano = 2023, created_at = DateTime.Now });
                context.SaveChanges();
            }

            // PROJETOPORTFOLIO
            if (!context.ProjetosPortfolio.Any())
            {
                var portfolio = context.Portfolios.First();
                context.ProjetosPortfolio.Add(new ProjetoPortfolio { PortfolioId = portfolio.PortfolioId, Categoria = "Web", Titulo = "Projeto Pessoal", Cliente = "Cliente X", data_projeto = DateTime.Now.AddMonths(-6), url_projeto = "http://projeto.com", Descricao = "Sistema de controle", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // IMAGEMPROJETO
            if (!context.ImagensProjetos.Any())
            {
                var portfolio = context.Portfolios.First();
                context.ImagensProjetos.Add(new ImagemProjeto { PortfolioId = portfolio.PortfolioId, imagem_url = "imagem1.png", Descricao = "Imagem do projeto", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // SERVIÇOS
            if (!context.Servicos.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Servicos.Add(new Servico { PortfolioId = portfolio.PortfolioId, Titulo = "Consultoria", Icone = "icone.png", Descricao = "Consultoria em TI", Duracao = "1 mês", Gerente = "João Silva", ContatoSuporte = "suporte@techangola.com", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // FUNCIONALIDADES
            if (!context.Funcionalidades.Any())
            {
                var portfolio = context.Portfolios.First();
                context.Funcionalidades.Add(new Funcionalidade { PortfolioId = portfolio.PortfolioId, Tipo = "Login", referencia_id = 1, Titulo = "Login Social", Icone = "login.png", Descricao = "Login com Google e Facebook", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // REDESOCIAL
            if (!context.RedesSociais.Any())
            {
                var portfolio = context.Portfolios.First();
                context.RedesSociais.Add(new RedeSocial { PortfolioId = portfolio.PortfolioId, Plataforma = "LinkedIn", URL = "https://linkedin.com/in/joao", Icone = "linkedin.png", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // SERVIÇOSRELACIONADOS
            if (!context.ServicosRelacionados.Any())
            {
                var servico = context.Servicos.First();
                context.ServicosRelacionados.Add(new ServicosRelacionados { servico_id = servico.id, relacionado_id = servico.id });
                context.SaveChanges();
            }

            // MÉTRICAS
            if (!context.Metricas.Any())
            {
                context.Metricas.Add(new Metrica { tipo = TipoMetrica.Usuarios, valor = 100, data = DateTime.Now, descricao = "Total de usuários", created_at = DateTime.Now });
                context.SaveChanges();
            }

            // SESSÕES
            if (!context.Sessoes.Any())
            {
                var usuario = context.Usuarios.First();
                context.Sessoes.Add(new Sessao {
                    Usuario = usuario,
                    token = "token123",
                    data_inicio = DateTime.Now,
                    data_expiracao = DateTime.Now.AddHours(1),
                    dispositivo = "Web",
                    status = StatusSessao.Ativo,
                    created_at = DateTime.Now
                });
                context.SaveChanges();
            }

            // LOGS
            if (!context.Logs.Any())
            {
                var usuario = context.Usuarios.First(); // Pega um usuário já salvo
                context.Logs.Add(new Log {
                    Usuario = usuario,
                    acao = "Login",
                    detalhes = "Usuário realizou login",
                    data_hora = DateTime.Now,
                    ip = "127.0.0.1",
                    created_at = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
} }