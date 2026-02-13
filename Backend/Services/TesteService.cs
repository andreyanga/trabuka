using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabukaApi.Data;
using TrabukaApi.Dtos;
using TrabukaApi.Interfaces;
using TrabukaApi.Models;
using TrabukaApi.Models.Enums;

namespace TrabukaApi.Services
{
    public class TesteService : ITesteService
    {
        private readonly ITesteRepository _testeRepository;
        private readonly IQuestaoRepository _questaoRepository;
        private readonly IResultadoTesteRepository _resultadoTesteRepository;
        private readonly TrabukaDbContext _context;

        public TesteService(
            ITesteRepository testeRepository,
            IQuestaoRepository questaoRepository,
            IResultadoTesteRepository resultadoTesteRepository,
            TrabukaDbContext context)
        {
            _testeRepository = testeRepository;
            _questaoRepository = questaoRepository;
            _resultadoTesteRepository = resultadoTesteRepository;
            _context = context;
        }

        public async Task<IEnumerable<TesteReadDto>> GetAllAsync()
        {
            var testes = await _testeRepository.GetAllAsync();
            var dtos = new List<TesteReadDto>();
            foreach (var teste in testes)
            {
                dtos.Add(await MapToReadDtoAsync(teste));
            }
            return dtos;
        }

        public async Task<TesteReadDto> GetByIdAsync(int id)
        {
            var teste = await _testeRepository.GetByIdAsync(id);
            return teste == null ? null : await MapToReadDtoAsync(teste);
        }

        public async Task<TesteReadDto> CreateAsync(TesteCreateDto dto)
        {
            var teste = new Teste
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao ?? "",
                Dificuldade = dto.Dificuldade,
                PontuacaoMaxima = dto.PontuacaoMaxima,
                PontuacaoMinima = dto.PontuacaoMinima,
                TempoLimiteMinutos = dto.TempoLimiteMinutos,
                Ativo = dto.Ativo,
                DataCriacao = DateTime.Now
            };
            await _testeRepository.AddAsync(teste);
            await _testeRepository.SaveChangesAsync();
            return await MapToReadDtoAsync(teste);
        }

        public async Task<TesteReadDto> UpdateAsync(int id, TesteUpdateDto dto)
        {
            var teste = await _testeRepository.GetByIdAsync(id);
            if (teste == null) return null;
            
            teste.Nome = dto.Nome;
            teste.Descricao = dto.Descricao ?? teste.Descricao;
            teste.Dificuldade = dto.Dificuldade;
            teste.PontuacaoMaxima = dto.PontuacaoMaxima;
            teste.PontuacaoMinima = dto.PontuacaoMinima;
            teste.TempoLimiteMinutos = dto.TempoLimiteMinutos;
            teste.Ativo = dto.Ativo;
            
            _testeRepository.Update(teste);
            await _testeRepository.SaveChangesAsync();
            return await MapToReadDtoAsync(teste);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var teste = await _testeRepository.GetByIdAsync(id);
            if (teste == null) return false;
            _testeRepository.Delete(teste);
            return await _testeRepository.SaveChangesAsync();
        }

        public async Task<TesteComQuestoesDto> IniciarTesteAsync(int testeId, int usuarioId)
        {
            // Verificar se o teste existe e está ativo
            var teste = await _testeRepository.GetByIdAsync(testeId);
            if (teste == null || !teste.Ativo)
                throw new ArgumentException("Teste não encontrado ou inativo");

            // Verificar se o usuário já fez este teste
            var resultadoAnterior = await _context.ResultadosTeste
                .FirstOrDefaultAsync(r => r.id_teste == testeId && r.id_usuario == usuarioId);
            
            if (resultadoAnterior != null)
                throw new InvalidOperationException("Usuário já realizou este teste");

            // Buscar questões do teste (sem mostrar a resposta correta)
            var questoes = await _questaoRepository.GetByTesteIdAsync(testeId);
            
            if (!questoes.Any())
                throw new InvalidOperationException("Teste não possui questões cadastradas");

            // Mapear questões para DTO (sem resposta correta)
            var questoesDto = questoes.Select(q => new QuestaoReadDto
            {
                QuestaoId = q.QuestaoId,
                TesteId = q.TesteId,
                Enunciado = q.Enunciado,
                OpcaoA = q.OpcaoA,
                OpcaoB = q.OpcaoB,
                OpcaoC = q.OpcaoC,
                OpcaoD = q.OpcaoD,
                Pontuacao = q.Pontuacao,
                Ordem = q.Ordem
            }).OrderBy(q => q.Ordem).ToList();

            return new TesteComQuestoesDto
            {
                TesteId = teste.TesteId,
                Nome = teste.Nome,
                Descricao = teste.Descricao,
                Dificuldade = teste.Dificuldade,
                TempoLimiteMinutos = teste.TempoLimiteMinutos,
                PontuacaoMaxima = teste.PontuacaoMaxima,
                PontuacaoMinima = teste.PontuacaoMinima,
                Questoes = questoesDto,
                DataInicio = DateTime.Now
            };
        }

        public async Task<ResultadoTesteCompletoDto> SubmeterTesteAsync(SubmeterTesteDto dto)
        {
            // Verificar se o teste existe
            var teste = await _testeRepository.GetByIdAsync(dto.TesteId);
            if (teste == null || !teste.Ativo)
                throw new ArgumentException("Teste não encontrado ou inativo");

            // Verificar se o usuário já fez este teste
            var resultadoAnterior = await _context.ResultadosTeste
                .FirstOrDefaultAsync(r => r.id_teste == dto.TesteId && r.id_usuario == dto.UsuarioId);
            
            if (resultadoAnterior != null)
                throw new InvalidOperationException("Usuário já realizou este teste");

            // Verificar tempo limite
            var tempoDecorrido = DateTime.Now - dto.DataInicio;
            if (tempoDecorrido.TotalMinutes > teste.TempoLimiteMinutos)
                throw new InvalidOperationException("Tempo limite excedido");

            // Buscar questões do teste com respostas corretas
            var questoes = await _questaoRepository.GetByTesteIdAsync(dto.TesteId);
            var questoesDict = questoes.ToDictionary(q => q.QuestaoId);

            // Calcular pontuação
            int pontuacaoObtida = 0;
            var respostasDetalhadas = new List<RespostaDetalhadaDto>();

            foreach (var resposta in dto.Respostas)
            {
                if (!questoesDict.ContainsKey(resposta.QuestaoId))
                    continue;

                var questao = questoesDict[resposta.QuestaoId];
                bool acertou = questao.RespostaCorreta.ToUpper() == resposta.RespostaEscolhida.ToUpper();
                
                if (acertou)
                    pontuacaoObtida += questao.Pontuacao;

                respostasDetalhadas.Add(new RespostaDetalhadaDto
                {
                    QuestaoId = questao.QuestaoId,
                    Enunciado = questao.Enunciado,
                    RespostaEscolhida = resposta.RespostaEscolhida,
                    RespostaCorreta = questao.RespostaCorreta,
                    Acertou = acertou,
                    PontuacaoObtida = acertou ? questao.Pontuacao : 0
                });
            }

            // Verificar se foi aprovado
            bool aprovado = pontuacaoObtida >= teste.PontuacaoMinima;

            // Atribuir nível baseado na dificuldade e aprovação
            NivelUsuario? nivelAtribuido = null;
            if (aprovado)
            {
                nivelAtribuido = teste.Dificuldade switch
                {
                    DificuldadeTeste.Inicial => NivelUsuario.Explorador,
                    DificuldadeTeste.Intermediario => NivelUsuario.Construtor,
                    DificuldadeTeste.Avancado => NivelUsuario.Mestre,
                    _ => null
                };
            }

            // Criar resultado do teste
            var resultado = new ResultadoTeste
            {
                id_teste = dto.TesteId,
                id_usuario = dto.UsuarioId,
                pontuacao = pontuacaoObtida,
                nivel_atribuido = nivelAtribuido,
                data_conclusao = DateTime.Now
            };

            await _resultadoTesteRepository.AddAsync(resultado);
            await _resultadoTesteRepository.SaveChangesAsync();

            // Atualizar nível do usuário se aprovado
            if (aprovado && nivelAtribuido.HasValue)
            {
                var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
                if (usuario != null)
                {
                    // Só atualiza se o novo nível for maior que o atual
                    if (!usuario.Nivel.HasValue || 
                        (int)nivelAtribuido.Value > (int)usuario.Nivel.Value)
                    {
                        usuario.Nivel = nivelAtribuido.Value;
                        _context.Usuarios.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            return new ResultadoTesteCompletoDto
            {
                ResultadoId = resultado.id_resultado,
                TesteId = teste.TesteId,
                NomeTeste = teste.Nome,
                UsuarioId = dto.UsuarioId,
                PontuacaoObtida = pontuacaoObtida,
                PontuacaoMaxima = teste.PontuacaoMaxima,
                PontuacaoMinima = teste.PontuacaoMinima,
                Aprovado = aprovado,
                NivelAtribuido = nivelAtribuido,
                DataConclusao = resultado.data_conclusao,
                RespostasDetalhadas = respostasDetalhadas
            };
        }

        public async Task<IEnumerable<TesteReadDto>> GetTestesDisponiveisAsync(int usuarioId)
        {
            // Buscar testes ativos
            var todosTestes = await _testeRepository.GetAllAsync();
            var testesAtivos = todosTestes.Where(t => t.Ativo).ToList();

            // Buscar testes já realizados pelo usuário
            var testesRealizados = await _context.ResultadosTeste
                .Where(r => r.id_usuario == usuarioId)
                .Select(r => r.id_teste)
                .ToListAsync();

            // Filtrar testes não realizados
            var testesDisponiveis = testesAtivos
                .Where(t => !testesRealizados.Contains(t.TesteId))
                .ToList();

            var dtos = new List<TesteReadDto>();
            foreach (var teste in testesDisponiveis)
            {
                dtos.Add(await MapToReadDtoAsync(teste));
            }
            return dtos;
        }

        private async Task<TesteReadDto> MapToReadDtoAsync(Teste teste)
        {
            // Contar questões do teste
            var totalQuestoes = await _questaoRepository.GetByTesteIdAsync(teste.TesteId);
            
            return new TesteReadDto
            {
                Id = teste.TesteId,
                TesteId = teste.TesteId,
                Nome = teste.Nome,
                Descricao = teste.Descricao,
                Dificuldade = teste.Dificuldade,
                PontuacaoMaxima = teste.PontuacaoMaxima,
                PontuacaoMinima = teste.PontuacaoMinima,
                TempoLimiteMinutos = teste.TempoLimiteMinutos,
                Ativo = teste.Ativo,
                DataCriacao = teste.DataCriacao,
                TotalQuestoes = totalQuestoes.Count()
            };
        }
    }
} 