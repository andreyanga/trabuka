import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface TesteReadDto {
  id: number;
  testeId: number;
  nome: string;
  descricao: string;
  dificuldade: string;
  pontuacaoMaxima: number;
  pontuacaoMinima: number;
  tempoLimiteMinutos: number;
  ativo: boolean;
  dataCriacao: string;
  totalQuestoes: number;
}

export interface QuestaoReadDto {
  questaoId: number;
  testeId: number;
  enunciado: string;
  opcaoA: string;
  opcaoB: string;
  opcaoC: string;
  opcaoD: string;
  pontuacao: number;
  ordem: number;
}

export interface TesteComQuestoesDto {
  testeId: number;
  nome: string;
  descricao: string;
  dificuldade: string;
  tempoLimiteMinutos: number;
  pontuacaoMaxima: number;
  pontuacaoMinima: number;
  questoes: QuestaoReadDto[];
  dataInicio: string;
}

export interface RespostaQuestaoDto {
  questaoId: number;
  respostaEscolhida: string; // A, B, C ou D
}

export interface SubmeterTesteDto {
  testeId: number;
  usuarioId: number;
  dataInicio: string;
  respostas: RespostaQuestaoDto[];
}

export interface RespostaDetalhadaDto {
  questaoId: number;
  enunciado: string;
  respostaEscolhida: string;
  respostaCorreta: string;
  acertou: boolean;
  pontuacaoObtida: number;
}

export interface ResultadoTesteCompletoDto {
  resultadoId: number;
  testeId: number;
  nomeTeste: string;
  usuarioId: number;
  pontuacaoObtida: number;
  pontuacaoMaxima: number;
  pontuacaoMinima: number;
  aprovado: boolean;
  nivelAtribuido: string | null;
  dataConclusao: string;
  respostasDetalhadas: RespostaDetalhadaDto[];
}

@Injectable({
  providedIn: 'root'
})
export class TesteService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  // Listar todos os testes
  getAllTestes(): Observable<TesteReadDto[]> {
    return this.http.get<TesteReadDto[]>(`${this.apiUrl}/Teste`);
  }

  // Obter testes disponíveis para um usuário
  getTestesDisponiveis(usuarioId: number): Observable<TesteReadDto[]> {
    return this.http.get<TesteReadDto[]>(`${this.apiUrl}/Teste/disponiveis/${usuarioId}`);
  }

  // Obter teste por ID
  getTesteById(id: number): Observable<TesteReadDto> {
    return this.http.get<TesteReadDto>(`${this.apiUrl}/Teste/${id}`);
  }

  // Iniciar um teste (obter questões)
  iniciarTeste(testeId: number, usuarioId: number): Observable<TesteComQuestoesDto> {
    return this.http.post<TesteComQuestoesDto>(`${this.apiUrl}/Teste/${testeId}/iniciar`, { usuarioId });
  }

  // Submeter teste (enviar respostas)
  submeterTeste(dto: SubmeterTesteDto): Observable<ResultadoTesteCompletoDto> {
    return this.http.post<ResultadoTesteCompletoDto>(`${this.apiUrl}/Teste/submeter`, dto);
  }
}
